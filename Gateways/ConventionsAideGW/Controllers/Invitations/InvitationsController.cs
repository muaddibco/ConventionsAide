using ConventionsAide.Core.Communication;
using ConventionsAide.Registrations.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConventionsAideGW.Controllers.Invitations
{
    [ApiController]
    [Route("/[controller]")]
    public class InvitationsController : ControllerBase
    {
        private readonly ICommunicationService _communicationService;

        public InvitationsController(ICommunicationService communicationService)
        {
            _communicationService = communicationService;
        }

        [HttpPost]
        [Authorize(Policy = "CreateInvitations")]
        public async Task<IActionResult> CreateInvitationFlow([FromBody] CreateInvitationFlowRequestDto request)
        {
            return Ok();
        }

        [HttpPut]
        [Authorize(Policy = "UpdateInvitations")]
        public async Task<IActionResult> UpdateInvitationFlow([FromBody] UpdateInvitationFlowWebRequestDto request)
        {
            return Ok();
        }

        [HttpGet]
        [Authorize(Policy = "ReadInvitations")]
        public async Task<IActionResult> GetInvitationFlow([FromQuery]long conventionId)
        {
            return Ok();
        }

        [HttpPost("{invitationId}/Registrations")]
        [Authorize(Policy = "CreateRegistrations")]
        public async Task<IActionResult> CreateTalkRegistration([FromRoute] long invitationId, [FromBody] CreateTalkRegistrationRequestDto request)
        {
            return Ok();
        }

        [HttpPut("{invitationId}/Registrations/{registrationId}")]
        [Authorize(Policy = "UpdateRegistrations")]
        public async Task<IActionResult> CreateTalkRegistration([FromRoute] long invitationId, [FromRoute] long registrationId, [FromBody] UpdateTalkRegistrationWebRequestDto request)
        {
            return Ok();
        }

        [HttpGet("{invitationId}/Registrations")]
        [Authorize(Policy = "ReadInvitationRegistrations")]
        public async Task<IActionResult> GetTalkRegistrations([FromQuery] long invitationId)
        {
            return Ok();
        }

        [HttpGet("/Registrations")]
        [Authorize(Policy = "ReadRegistrations")]
        public async Task<IActionResult> GetTalkRegistrationsByUser([FromQuery] long userId)
        {
            return Ok();
        }
    }
}
