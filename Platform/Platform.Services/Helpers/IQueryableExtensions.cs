using System;
using Platform.Services.Common;
using System.Linq;
using System.Linq.Dynamic;

namespace Platform.Services.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paging<T>(this IQueryable<T> data, Pagination pagination)
        {
            if (pagination == null)
            {
                return data;
            }

            if (data == null)
            {
                throw new Exception("Не удалось получить данные.");
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

            var elementType = data.ElementType;

            foreach (var filter in filters)
            {
                data = data.Where(filter.GetPredicateByType(elementType));
            }

            return data;
        }

    }
}
