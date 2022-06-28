using Microsoft.AspNetCore.Mvc;
using DesafioLaNacion.Services;
using DesafioLaNacion.Dtos;
using DesafioLaNacion.Models;
using System.Threading.Tasks;


namespace DesafioLaNacion.Controllers
{

    [Route("DesafioLaNacion/v1")]
    public class ContactRecordController : ApiBaseController
    {
        
        private readonly ContactRecordService _service;

        public ContactRecordController (ContactRecordService service)
        {
            _service = service;
        }
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(long id)
        {
            return Return(await _service.GetById(id).ConfigureAwait(false));
        }
        
        [HttpGet]
        [Route("GetByEmail")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            return Return(await _service.GetByEmail(email).ConfigureAwait(false));
        }
        
        [HttpGet]
        [Route("GetByNumber")]
        public async Task<IActionResult> GetByNumber(string number)
        {
            return Return(await _service.GetByNumber(number).ConfigureAwait(false));
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DtoAddContactRecord request)
        {
            return Return(await _service.Post(request).ConfigureAwait(false));
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] DtoEditContactRecord request)
        {
            return Return(await _service.Update(request).ConfigureAwait(false)); 
        }
        [HttpDelete]
        [Route("id")]
        public async Task<IActionResult> Delete(long id)
        {
            return Return(await _service.DeleteContactRecord(id).ConfigureAwait(false)); 
        }

        [HttpPost]
        [Route("list")]
        public async Task<IActionResult> ListByFilter (RequestPaginatedData request)
        {
            return Return(await _service.ListaCiudades(request.Filter, request.PageSize, request.Page).ConfigureAwait(false));
        }




    }
}
