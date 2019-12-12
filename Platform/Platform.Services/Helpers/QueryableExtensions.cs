using System;
using Platform.Services.Common;
using System.Linq;
using System.Linq.Dynamic;
using Platform.Services.Dto;

namespace Platform.Services.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paging<T>(this IQueryable<T> data, Pagination pagination)
        {
            if (data == null)
            {
                throw new Exception("Не удалось получить данные.");
            }

            if (pagination == null || pagination.RowsPerPage == 0)
            {
                return data;
            }

            return data
                .Skip((pagination.Page - 1) * pagination.RowsPerPage)
                .Take(pagination.RowsPerPage);
        }

        public static IQueryable<T> Filter<T>(this IQueryable<T> data, Filtration[] filters)
        {
            if (filters == null || !filters.Any())
            {
                return data;
            }

            if (data == null)
            {
                throw new Exception("Не удалось получить данные.");
            }

            var modelType = data.ElementType;

            foreach (var filter in filters)
            {
                if (!string.IsNullOrEmpty(filter.ColumnValue))
                {
                    var propertyType = modelType
                        .GetProperties()
                        .FirstOrDefault(x => x.Name.ToLower() == filter.ColumnName.ToLower())?.PropertyType 
                        ?? throw new Exception("Не удалось получить фильтруемое свойство.");

                    data = data.Where(filter.GetPredicateByFilter(propertyType));
                }
            }

            return data;
        }

        public static IQueryable<T> Order<T>(this IQueryable<T> data, Sorting sorting) 
            where T: IEntityDto
        {
            if (sorting == null)
            {
                return data.OrderBy("x=>x.Id");
            }

            if (data == null)
            {
                throw new Exception("Не удалось получить данные.");
            }

            if (!data.ElementType.GetProperties()
                .Any(x => x.Name.ToLower() == sorting.ColumnName.ToLower()))
            {
                throw new Exception("Не удалось получить фильтруемое свойство.");
            }

            data = sorting.Ascending
                ? data.OrderBy(sorting.GetPredicateBySorting())
                : data.OrderByDescending(sorting.GetPredicateBySorting());

            return data;
        }
    }
}