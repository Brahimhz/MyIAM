using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyIAM.AppService.Contracts;
using MyIAM.AppService.Resources.MyApiScope;
using MyIAM.Domain;

namespace MyIAM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiScopesController : ControllerBase
    {
        private readonly IGenericAppService<MyApiScope, MyApiScopeOutPut, MyApiScopeListOutPut, MyApiScopeInPut> _appService;

        public ApiScopesController(IGenericAppService<MyApiScope, MyApiScopeOutPut,MyApiScopeListOutPut, MyApiScopeInPut> appService)
        {
            _appService = appService;
        }

        [HttpPost(Name = "CreateApiScope")]
        public async Task<IActionResult> CreateApiScope([FromBody] MyApiScopeInPut input)
        {
            var result = await _appService.Add(input);
            if (result is null) return BadRequest();
            else return Ok(result);
        }

        [HttpPut(Name = "ModifyApiScope/{id}")]
        public async Task<IActionResult> ModifyApiScope(Guid id, [FromBody] MyApiScopeInPut input)
        {
            var result = await _appService.Modify(id, input);
            if (result is null) return BadRequest();
            else return Ok(result);
        }

        [HttpDelete(Name = "DeleteApiScope/{id}")]
        public async Task<IActionResult> DeleteApiScope(Guid id)
        {
            var result = await _appService.Delete(id);
            return Ok(result);
        }

        [HttpGet("ApiScope/{id}", Name = "GetApiScopeById")]
        public async Task<IActionResult> GetApiScopeById(Guid id)
        {
            var result = await _appService.GetById(id);
            if (result is null) return BadRequest();
            return Ok(result);
        }

        [HttpGet("ApiScopes", Name = "GetApiScopes")]
        public async Task<IActionResult> GetApiScopes()
        {
            var result = await _appService.GetAll();
            if (result is null) return BadRequest();
            return Ok(result);
        }
    }
}
