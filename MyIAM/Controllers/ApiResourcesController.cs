using Microsoft.AspNetCore.Mvc;
using MyIAM.AppService.Contracts;
using MyIAM.AppService.Resources.MyApiResource;
using MyIAM.Domain;

namespace MyIAM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiResourcesController : ControllerBase
    {
        private readonly IGenericAppService<MyApiResource, MyApiResourceOutPut, MyApiResourceInPut> _appService;

        public ApiResourcesController(IGenericAppService<MyApiResource, MyApiResourceOutPut, MyApiResourceInPut> appService)
        {
            _appService = appService;
        }

        [HttpPost(Name = "CreateApiResource")]
        public async Task<IActionResult> CreateApiResource([FromBody] MyApiResourceInPut input)
        {
            var result = await _appService.Add(input);
            if (result is null) return BadRequest();
            else return Ok(result);
        }

        [HttpPut(Name = "ModifyApiResource/{id}")]
        public async Task<IActionResult> ModifyApiResource(Guid id, [FromBody] MyApiResourceInPut input)
        {
            var result = await _appService.Modify(id, input);
            if (result is null) return BadRequest();
            else return Ok(result);
        }

        [HttpDelete(Name = "DeleteApiResource/{id}")]
        public async Task<IActionResult> DeleteApiResource(Guid id)
        {
            var result = await _appService.Delete(id);
            return Ok(result);
        }

        [HttpGet("ApiResource/{id}", Name = "GetApiResourceById")]
        public async Task<IActionResult> GetApiResourceById(Guid id)
        {
            var result = await _appService.GetById(id);
            if (result is null) return BadRequest();
            return Ok(result);
        }

        [HttpGet("ApiResources", Name = "GetApiResources")]
        public async Task<IActionResult> GetApiResources()
        {
            var result = await _appService.GetAll();
            if (result is null) return BadRequest();
            return Ok(result);
        }
    }
}
