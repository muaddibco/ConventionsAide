using ConventionsAide.Core.Communication;
using ConventionsAide.Users.Client;
using ConventionsAide.Users.Contracts;
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

        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestDto request, CancellationToken cancellationToken)
        {
            return Ok(await _communicationService.CreateUserAsync(request));
        }
    }
}
