using ConventionsAide.Core.Contracts;
using ConventionsAide.Users.Domain.Shared;
using ConventionsAide.VenueOrders.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConventionsAide.Registrations.Contracts
{
    public class UpdateInvitationFlowWebRequestDto
    {
        public InvitationsFlowStatus Status { get; set; }
    }
}
