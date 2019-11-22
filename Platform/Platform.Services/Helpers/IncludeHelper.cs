using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Platform.Fodels.Enums;
using Platform.Fodels.Models.Address;

namespace Platform.Services.Helpers
{
	public static class IncludeHelper
	{
		public static IQueryable<T> IncludeAllAddressItems<T>(this IQueryable<T> source) where T : class, IAddressElement
		{
			var addressTypes = TypeHelper.GetTypes(typeof(IAddressElement));
			var curElemType = source.ElementType;
			var first = true;
			var operationHealth = (IIncludableQueryable<T,T>)source;
			while (curElemType != null && curElemType != typeof(Country))
			{
				var addressItem = Enum.Parse<AddressItem>(curElemType.Name);
				var parent = addressItem.GetParentPropertyName();
				if (parent == null)
					return operationHealth;
				if (first)
				{
					ParameterExpression pe = Expression.Parameter(typeof(T), "source");
					Expression lastMember = pe;
					MemberExpression member = Expression.Property(lastMember, "Country");
					Expression<Func<T, T>> lambda = Expression.Lambda<Func<T, T>>(member);
					operationHealth = source.Include(lambda);
					first = false;
				}
				else
				{
					ParameterExpression pe = Expression.Parameter(typeof(T), "source");
					Expression lastMember = pe;
					MemberExpression member = Expression.Property(lastMember, "Country");
					Expression<Func<T, T>> lambda = Expression.Lambda<Func<T, T>>(member);
					operationHealth = operationHealth.ThenInclude(lambda);
				}
				curElemType = addressTypes.FirstOrDefault(x => x.Name == parent);
			}

			return operationHealth;
		}
	}
}