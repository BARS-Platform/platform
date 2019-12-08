namespace Platform.Services.Common
{
    public class ListParam
    {
        public Pagination Pagination{ get; set; }

        public Filtration[] Filters { get; set; }

        public Sorting Sorting { get; set; }
    }
}
