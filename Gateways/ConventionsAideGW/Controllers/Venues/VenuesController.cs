using ConventionsAide.Core.Communication;
using Microsoft.AspNetCore.Mvc;
using ConventionsAide.Venues.Client;
using ConventionsAide.Venues.Contracts;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Policy = "ReadVenues")]
        public async Task<IActionResult> GetVenues([FromQuery] GetVenuesListRequestDto request, CancellationToken cancellationToken = default)
        {
            var response = await _communicationService.GetVenuesList(request);

            return Ok(response);
        }
    }
}
