namespace Platform.Domain.Common
{
    public class OperationResult
    {
        public OperationResult()
        {
        }

        public OperationResult(bool success)
        {
            Success = success;
        }

        public OperationResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public OperationResult(bool success, object data)
        {
            Success = success;
            Data = data;
        }

        /// <summary>
        /// Признак успешности операции.
        /// </summary>
        public bool Success { get; set; }
        
        /// <summary>
        /// Сообщение об ошибке.
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// Данные.
        /// </summary>
        public object Data { get; set; }
    }
}
