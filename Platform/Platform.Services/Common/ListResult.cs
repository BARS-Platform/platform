using Platform.Services.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Platform.Services.Common
{
    public class ListResult<T>
    {
        public IEnumerable<T> Data { get; set; }

        public int TotalCount { get; set; }
    }

    public static class ListResult
    {
        public static ListResult<T> FormData<T>(this IQueryable<T> query, ListParam listParam)
        {
            return new ListResult<T>
            {
                Data = query
                    .Paging(listParam.Pagination)
                    .ToList(),
                TotalCount =  query.Count()
            };
        }
    }
}
