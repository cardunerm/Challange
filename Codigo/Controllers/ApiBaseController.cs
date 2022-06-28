using Microsoft.AspNetCore.Mvc;
using DesafioLaNacion.Infraestructura;

namespace DesafioLaNacion.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    
    public class ApiBaseController : ControllerBase
    {

        //Preguntar acá que onda con la ruta que tiene
        public IActionResult Return<T>(OperationResponse<T> response)
        {
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.Exception);   
        }
    }
}
