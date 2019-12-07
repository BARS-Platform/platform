using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Services.Common;

namespace Platform.Services.Helpers
{
    public static class ListParamExtensions
    {
        public static string GetPredicateByFilter(this Filtration filter, Type propertyType)
        {
            var predicate = $"x => x.{filter.ColumnName}";
            

            var hasEqualityOperator = filter.ColumnValue.First() == '=';

            if (propertyType == typeof(int) 
                || propertyType == typeof(long)
                || propertyType == typeof(decimal))
            {
                return filter.ColumnOperator == "="
                    ? $"{predicate}=={filter.ColumnValue}"
                    : $"{predicate}{filter.ColumnOperator}{filter.ColumnValue}";
            }

            if (propertyType == typeof(string))
            {
                return predicate + $@".ToLower().Contains(""{filter.ColumnValue}"")";
            }

            if (propertyType == typeof(DateTime))
            {
                return predicate + $@".ToShortDateString() == {filter.ColumnValue}";
            }

            return "x => true";
        }

        public static string GetPredicateBySorting(this Sorting sorting)
        {
            return $@"x => x.{sorting.ColumnName}";
        }
    }
}
