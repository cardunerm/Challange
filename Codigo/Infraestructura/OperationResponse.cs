namespace DesafioLaNacion.Infraestructura
{
    public class OperationResponse<T>
    {
        public OperationResponse(T data, bool success = true, string ex = null)
        {
            Data = data;
            Success = success;
            Exception = ex;
        }
        public bool Success { get; set; }
        public string Exception { get; set; }
        public T Data { get; set; }
    }

}
