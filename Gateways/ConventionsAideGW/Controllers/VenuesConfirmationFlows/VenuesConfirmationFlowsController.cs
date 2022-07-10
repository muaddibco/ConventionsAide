using ConventionsAide.Core.Communication;
using ConventionsAide.VenueOrders.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConventionsAideGW.Controllers.VenuesConfirmationFlows
{
    [ApiController]
    [Route("/[controller]")]
    public class VenuesConfirmationFlowsController : ControllerBase
    {
        private readonly ICommunicationService _communicationService;

        public VenuesConfirmationFlowsController(ICommunicationService communicationService)
        {
            _communicationService = communicationService;
        }

        [HttpPost]
        [Authorize(Policy = "CreateVenuesConfirmationFlows")]
        public async Task<IActionResult> CreateVenuesConfirmationFlow([FromBody] CreateVenuesConfirmationFlowRequestDto request)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "UpdateVenuesConfirmationFlows")]
        public async Task<IActionResult> UpdateVenuesConfirmationFlow([FromRoute] long id, [FromBody] UpdateVenuesConfirmationFlowWebRequestDto request)
        {
            return Ok();
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ReadVenuesConfirmationFlows")]
        public async Task<IActionResult> GetVenuesConfirmationFlow([FromRoute] long id)
        {
            return Ok();
        }

        [HttpPost("{flowId}/Orders")]
        [Authorize(Policy = "CreateVenuesConfirmationFlows")]
        public async Task<IActionResult> CreateVenuesOrder([FromBody] CreateVenueOrderWebRequestDto request)
        {
            return Ok();
        }

        [HttpPut("{flowId}/Orders/{orderId}")]
        [Authorize(Policy = "UpdateVenuesConfirmationFlows")]
        public async Task<IActionResult> UpdateVenuesOrder([FromRoute] long flowId, [FromRoute] long orderId, [FromBody] UpdateVenueOrderWebRequestDto request)
        {
            return Ok();
        }
    }
}
