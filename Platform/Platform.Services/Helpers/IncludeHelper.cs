using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Platform.Fatabase;
using Platform.Fodels.Enums;
using Platform.Fodels.Models.Address;

namespace Platform.Services.Helpers
{
	public static class IncludeHelper
	{
		public static IQueryable<Apartment> IncludeAll(this IQueryable<Apartment> source)
		{
			return source
				.Include(apartment => apartment.House)
				.ThenInclude(house => house.Street)
				.ThenInclude(street => street.City)
				.ThenInclude(city => city.State)
				.ThenInclude(state => state.Country);
		}
		
		public static IQueryable<House> IncludeAll(this IQueryable<House> source)
		{
			return source
				.Include(house => house.Street)
				.ThenInclude(street => street.City)
				.ThenInclude(city => city.State)
				.ThenInclude(state => state.Country);
		}
		
		public static IQueryable<Street> IncludeAll(this IQueryable<Street> source)
		{
			return source
				.Include(street => street.City)
				.ThenInclude(city => city.State)
				.ThenInclude(state => state.Country);
		}
		
		public static IQueryable<City> IncludeAll(this IQueryable<City> source)
		{
			return source
				.Include(city => city.State)
				.ThenInclude(state => state.Country);
		}
		
		public static IQueryable<State> IncludeAll(this IQueryable<State> source)
		{
			return source
				.Include(state => state.Country);
		}
		
		public static IQueryable<T> IncludeAllAddressItems<T>(this IQueryable<T> source) where T : class, IAddressElement
		{
			var addressTypes = TypeHelper.GetTypes(typeof(IAddressElement));
			var curElemType = source.ElementType;
			var initialQueryType = source.ElementType;
			var first = true;
			object operationHealth = null;
			while (curElemType != null && curElemType != typeof(Country))
			{
				var addressItem = Enum.Parse<AddressItem>(curElemType.Name);
				var parent = addressItem.GetParentPropertyName();
				if (parent == null)
					return (IQueryable<T>) operationHealth ?? source;
				var parentType = addressTypes.FirstOrDefault(x => x.Name == parent);
				if (first)
				{
					var lambda = MakeFuncLambdaExpressions<T>(curElemType, parent, parentType);

					var include = typeof(EntityFrameworkQueryableExtensions)
						.GetMethods()
						.Single(methodInfo =>
							methodInfo.Name == "Include" &&
							methodInfo.GetParameters()[1].ParameterType != typeof(string))
						.MakeGenericMethod(curElemType, parentType);
					operationHealth = include.Invoke(null, new[] {source, lambda});

					first = false;
				}
				else
				{
					var lambda = MakeFuncLambdaExpressions<T>(curElemType, parent, parentType);

					var include = typeof(EntityFrameworkQueryableExtensions)
						.GetMethods()
						.Single(methodInfo =>
							methodInfo.Name == "ThenInclude"
							&& !methodInfo.GetParameters()[0].ParameterType.GenericTypeArguments[1].IsGenericType);
					var includeGeneric = include.MakeGenericMethod(initialQueryType, curElemType, parentType);
					operationHealth = includeGeneric.Invoke(null, new[] {operationHealth, lambda});
				}

				curElemType = parentType;
			}
 
			return (IQueryable<T>) operationHealth;
		}

		private static object MakeFuncLambdaExpressions<T>(Type curElemType, string parent, Type parentType)
			where T : class, IAddressElement
		{
			ParameterExpression pe = Expression.Parameter(curElemType, "source");
			Expression lastMember = pe;
			MemberExpression member = Expression.Property(lastMember, parent);

			var expressionLambdaMethod = typeof(Expression).GetMethods()
				.Single(methodInfo =>
				{
					if (!methodInfo.IsGenericMethod)
						return false;
					var parameters = methodInfo.GetParameters();
					if (parameters.Length != 2)
						return false;
					if (parameters[0].ParameterType != typeof(Expression))
						return false;
					if (parameters[1].ParameterType != typeof(ParameterExpression[]))
						return false;
					return true;
				});

			var genericFunc = typeof(Func<,>).MakeGenericType(curElemType, parentType);
			var genericMethod = expressionLambdaMethod.MakeGenericMethod(genericFunc);

			var lambda = genericMethod.Invoke(null, new object[] {member, new[] {pe}});
			return lambda;
		}
	}
}