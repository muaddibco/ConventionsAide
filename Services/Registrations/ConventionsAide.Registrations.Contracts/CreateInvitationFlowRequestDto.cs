using ConventionsAide.Users.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConventionsAide.Registrations.Contracts
{
    public class CreateInvitationFlowRequestDto
    {
        public long ConventionId { get; set; }
        public UserTier? Tier { get; set; }
    }
}
