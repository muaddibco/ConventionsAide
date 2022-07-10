using ConventionsAide.Core.Communication;
using ConventionsAide.Venues.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConventionsAideGW.Controllers.Users
{
    [ApiController]
    [Route("/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ICommunicationService _communicationService;

        public UsersController(ICommunicationService communicationService)
        {
            _communicationService = communicationService;
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "UpdateUsers")]
        public async Task<IActionResult> UpdateUserTier([FromBody] UpdateUserTierWebRequestDto request)
        {
            return Ok();
        }

        [HttpGet]
        [Authorize(Policy = "ReadUsers")]
        public async Task<IActionResult> GetUsers([FromQuery] GetUsersListRequestDto request)
        {
            return Ok();
        }
    }
}
