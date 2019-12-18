using System.Collections.Generic;

namespace Platform.Services.Common
{
    public class ListParam
    {
        public Pagination Pagination{ get; set; }

        public List<Filtration> Filters { get; set; }

        public Sorting Sorting { get; set; }
    }
}
