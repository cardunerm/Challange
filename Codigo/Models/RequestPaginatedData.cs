namespace DesafioLaNacion.Models
{
    public class RequestPaginatedData
    {
        public string Filter { get; set; } = "";
        public int Page { get; set; } = 0;
        public int PageSize { get; set; } = 10;
    }
}
