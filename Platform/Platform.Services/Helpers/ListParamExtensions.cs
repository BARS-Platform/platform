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
            filter.ConverFilter();

            var predicate = $"x => x.{filter.ColumnName}";

            if (propertyType == typeof(int) 
                || propertyType == typeof(long)
                || propertyType == typeof(decimal)
                || propertyType == typeof(double)
                || propertyType == typeof(float))
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

        private static void ConverFilter(this Filtration filter)
        {
            filter.ColumnName = filter.ColumnName.First().ToString().ToUpper() 
                                + filter.ColumnName.Substring(1);

            filter.ColumnValue = filter.ColumnValue.ToLower();
        }
    }
}
