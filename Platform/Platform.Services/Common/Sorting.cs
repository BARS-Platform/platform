namespace Platform.Services.Common
{
    public class Sorting
    {
        /// <summary>
        /// Наименование колонки в реестре,
        /// совпадает с названием свойства в DTO.
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Направление сортировки,
        /// по возрастанию - true,
        /// по убыванию - false.
        /// </summary>
        public bool Ascending { get; set; }
    }
}
