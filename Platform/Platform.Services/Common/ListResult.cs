using Platform.Services.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Platform.Services.Common
{
    public class ListResult<T>
    {
        public IEnumerable<T> Data { get; set; }

<<<<<<< HEAD
        public int TotalCount { get; set; }
=======
        public Pagination Pagination { get; set; }
>>>>>>> Реализована пагинация на сервере
    }

    public static class ListResult
    {
<<<<<<< HEAD
        public static ListResult<T> FormData<T>(this IQueryable<T> query, ListParam listParam)
=======
        public static ListResult<T> FormData<T>(this IQueryable<T> query, ListParam listParam) 
>>>>>>> Реализована пагинация на сервере
        {
            return new ListResult<T>
            {
                Data = query
                    .Paging(listParam.Pagination)
                    .ToList(),
<<<<<<< HEAD
                TotalCount =  query.Count()
=======
                Pagination = new Pagination()
                {
                    Page = listParam.Pagination.Page,
                    RowsPerPage = listParam.Pagination.RowsPerPage,
                    RowsNumber = query.Count()
                }
>>>>>>> Реализована пагинация на сервере
            };
        }
    }
}
