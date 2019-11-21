using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Platform.Services
{
	public class TypeHelper
	{
		public static Type[] GetTypes(Type parameterType)
		{
			if (parameterType == null)
				throw new ArgumentNullException(nameof(parameterType));

			return AppDomain.CurrentDomain
				.GetAssemblies()
				.Where(x => !x.IsDynamic)
				.SelectMany(x => x.GetExportedTypes()
					.Where(y => y.IsClass)
					.Where(parameterType.IsAssignableFrom))
				.ToArray();
		}

		public List<T> GetAttributes<T>() where T : Attribute
		{
			return AppDomain.CurrentDomain
				.GetAssemblies()
				.Where(x => !x.IsDynamic)
				.SelectMany(x => x.GetExportedTypes())
				.Select(x => (T) x.GetCustomAttribute(typeof(T), true))
				.Where(x => x != null)
				.ToList();
		}
	}
}