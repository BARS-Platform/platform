namespace Platform.Services.Common
{
    public class Pagination
    {
        /// <summary>
        /// Номер отображаемой страницы.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Количество строк для отображения.
        /// </summary>
        public int RowsPerPage { get; set; }

        /// <summary>
        /// Общее количество строк
        /// </summary>
        public int RowsNumber { get; set; }
    }
}
