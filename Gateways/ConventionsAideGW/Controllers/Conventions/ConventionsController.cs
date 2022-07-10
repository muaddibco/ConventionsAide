using ConventionsAide.Conventions.Contracts;
using ConventionsAide.Core.Communication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConventionsAideGW.Controllers.Conventions
{
    [ApiController]
    [Route("/[controller]")]
    public class ConventionsController : ControllerBase
    {
        private readonly ICommunicationService _communicationService;

        public ConventionsController(ICommunicationService communicationService)
        {
            _communicationService = communicationService;
        }

        [HttpPost]
        [Authorize(Policy = "CreateConventions")]
        public async Task<IActionResult> CreateConvention([FromBody] CreateConventionRequestDto request)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "UpdateConventions")]
        public async Task<IActionResult> UpdateConvention([FromRoute] long id, [FromBody] UpdateConventionWebRequestDto request)
        {
            return Ok();
        }

        [HttpPatch("{id}")]
        [Authorize(Policy = "UpdateConventions")]
        public async Task<IActionResult> PatchConvention([FromRoute] long id, [FromBody] UpdateConventionWebRequestDto request)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "DeleteConventions")]
        public async Task<IActionResult> DeleteConvention([FromRoute] long id)
        {
            return Ok();
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ReadConventions")]
        public async Task<IActionResult> GetConvention([FromRoute] long id)
        {
            return Ok();
        }

        [HttpGet]
        [Authorize(Policy = "ReadConventions")]
        public async Task<IActionResult> GetConventions([FromQuery] GetConventionsListRequestDto request)
        {
            return Ok();
        }
    }
}
