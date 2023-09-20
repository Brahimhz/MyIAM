using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyIAM.AppService.Contracts;
using MyIAM.AppService.Resources.MyClient;
using MyIAM.Domain;

namespace MyIAM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IGenericAppService<MyClient, MyClientOutPut, MyClientInPut> _appService;

        public ClientsController(IGenericAppService<MyClient, MyClientOutPut, MyClientInPut> appService)
        {
            _appService = appService;
        }

        [HttpPost(Name = "CreateClient")]
        public async Task<IActionResult> CreateClient([FromBody] MyClientInPut input)
        {
            var result = await _appService.Add(input);
            if (result is null) return BadRequest();
            else return Ok(result);
        }

        [HttpPut(Name = "ModifyClient/{id}")]
        public async Task<IActionResult> ModifyClient(Guid id, [FromBody] MyClientInPut input)
        {
            var result = await _appService.Modify(id, input);
            if (result is null) return BadRequest();
            else return Ok(result);
        }

        [HttpDelete(Name = "DeleteClient/{id}")]
        public async Task<IActionResult> DeleteClient(Guid id)
        {
            var result = await _appService.Delete(id);
            return Ok(result);
        }

        [HttpGet("Client/{id}", Name = "GetClientById")]
        public async Task<IActionResult> GetClientById(Guid id)
        {
            var result = await _appService.GetById(id);
            if(result is null) return BadRequest();
            return Ok(result);
        }

        [HttpGet("Clients", Name = "GetClients")]
        public async Task<IActionResult> GetClients()
        {
            var result = await _appService.GetAll();
            if (result is null) return BadRequest();
            return Ok(result);
        }
    }
}
