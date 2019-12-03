using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Services.Common;

namespace Platform.Services.Helpers
{
    public static class FiltrationExtension
    {
        public static string GetPredicateByType(this Filtration filter, Type modelType)
        {
            var predicate = $"x => x.{filter.ColumnName}";
            var propertyInfo = modelType
                                   .GetProperties()
                                   .FirstOrDefault(x => x.Name.ToLower() == filter.ColumnName.ToLower())
                               ?? throw new Exception("Не удалось получить фильтруемое свойство");

            var propertyType = propertyInfo.PropertyType;

            var hasComparisonOperator = filter.ColumnValue.Contains(">") 
                                        || filter.ColumnValue.Contains("<");

            if (propertyType == typeof(int) 
                || propertyType == typeof(long)
                || propertyType == typeof(decimal))
            {
                if (hasComparisonOperator)
                {
                    return predicate + filter.ColumnValue;
                }
                return predicate + $" == {filter.ColumnValue}";
            }

            if (propertyType == typeof(string))
            {
                return predicate + $@".Contains(""{filter.ColumnValue}"")";
            }

            if (propertyType == typeof(DateTime))
            {
                return predicate + $@".ToShortDateString() == {filter.ColumnValue}";
            }

            return "x => true";
        }
    }
}
