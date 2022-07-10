using ConventionsAide.Core.Communication;
using Microsoft.AspNetCore.Mvc;
using ConventionsAide.Venues.Client;
using ConventionsAide.Venues.Contracts;

namespace ConventionsAideGW.Controllers.Venues
{
    [ApiController]
    [Route("/[controller]")]
    public class VenuesController : ControllerBase
    {
        private readonly ICommunicationService _communicationService;

        public VenuesController(ICommunicationService communicationService)
        {
            _communicationService = communicationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetVenues([FromQuery] GetVenuesListRequestDto request, CancellationToken cancellationToken = default)
        {
            var response = await _communicationService.GetVenuesList(request);

            return Ok(response);
        }
    }
}
