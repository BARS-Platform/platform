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
<<<<<<< HEAD
        /// Общее количество строк.
=======
        /// Общее количество строк
>>>>>>> Реализована пагинация на сервере
        /// </summary>
        public int RowsNumber { get; set; }
    }
}
