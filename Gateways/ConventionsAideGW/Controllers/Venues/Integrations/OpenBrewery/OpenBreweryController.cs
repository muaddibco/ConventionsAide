using ConventionsAide.Core.Communication;
using Microsoft.AspNetCore.Mvc;
using ConventionsAide.Venues.Integration.OpenBrewery.Client;
using ConventionsAide.Venues.Integration.OpenBrewery.Contracts;

namespace ConventionsAideGW.Controllers.Venues.Integrations.OpenBrewery
{
    [ApiController]
    [Route("/venues/integrations/[controller]")]
    public class OpenBreweryController : ControllerBase
    {
        private readonly ICommunicationService _communicationService;

        public OpenBreweryController(ICommunicationService communicationService)
        {
            _communicationService = communicationService;
        }

        [HttpPost]
        public async Task<IActionResult> Sync(CancellationToken cancellationToken)
        {
            var response = await  _communicationService.SyncBrewerysAsync(new SyncBrewerysRequestDto());

            return Ok(response);
        }
    }
}
