using Platform.Services.Common;
using System.Linq;

namespace Platform.Services.Helpers
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Paging<T>(this IQueryable<T> data, Pagination pagination)
        {
            if (pagination == null)
            {
                return data;
            }

            return data
                .Skip((pagination.Page - 1) * pagination.RowsPerPage)
                .Take(pagination.RowsPerPage);
        }
    }
}
