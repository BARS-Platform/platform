namespace Platform.Services.Common
{
    public class Filtration
    {
        /// <summary>
        /// Наименование колонки в реестре,
        /// совпадает с названием свойства в DTO.
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Значение оператора сравнения.
        /// </summary>
        public string ColumnOperator{ get; set; }

        /// <summary>
        /// Значение фильтра.
        /// </summary>
        public string ColumnValue { get; set; }
    }
}
