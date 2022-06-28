using System.Collections.Generic;
namespace DesafioLaNacion.Dtos
{
    public class DtoResponsePagination<T> 
    {
        public IEnumerable<T> Data { get; set; }
        public long TotalCount { get; set; }
        public int PageSize { get; set; }
    }
}
