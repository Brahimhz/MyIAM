using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyIAM.AppService.Contracts;
using MyIAM.AppService.Resources.MyIdentityResource;
using MyIAM.Domain;

namespace MyIAM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityResourcesController : ControllerBase
    {
        private readonly IGenericAppService<MyIdentityResource, MyIdentityResourceOutPut, MyIdentityResourceInPut> _appService;

        public IdentityResourcesController(IGenericAppService<MyIdentityResource, MyIdentityResourceOutPut, MyIdentityResourceInPut> appService)
        {
            _appService = appService;
        }

        [HttpPost(Name = "CreateIdentityResource")]
        public async Task<IActionResult> CreateIdentityResource([FromBody] MyIdentityResourceInPut input)
        {
            var result = await _appService.Add(input);
            if (result is null) return BadRequest();
            else return Ok(result);
        }

        [HttpPut(Name = "ModifyIdentityResource/{id}")]
        public async Task<IActionResult> ModifyIdentityResource(Guid id, [FromBody] MyIdentityResourceInPut input)
        {
            var result = await _appService.Modify(id, input);
            if (result is null) return BadRequest();
            else return Ok(result);
        }

        [HttpDelete(Name = "DeleteIdentityResource/{id}")]
        public async Task<IActionResult> DeleteIdentityResource(Guid id)
        {
            var result = await _appService.Delete(id);
            return Ok(result);
        }

        [HttpGet("IdentityResource/{id}", Name = "GetIdentityResourceById")]
        public async Task<IActionResult> GetIdentityResourceById(Guid id)
        {
            var result = await _appService.GetById(id);
            if (result is null) return BadRequest();
            return Ok(result);
        }

        [HttpGet("IdentityResources", Name = "GetIdentityResources")]
        public async Task<IActionResult> GetIdentityResources()
        {
            var result = await _appService.GetAll();
            if (result is null) return BadRequest();
            return Ok(result);
        }
    }
}
