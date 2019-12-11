using Platform.Services.Dto;
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
            where T : IEntityDto
        {

            var filteredQuery = query.Filter(listParam.Filters);
            return new ListResult<T>
            {
                Data = filteredQuery
                    .Order(listParam.Sorting)
                    .Paging(listParam.Pagination).ToList(),
                TotalCount = filteredQuery.Count()
            };
        }
    }
}
