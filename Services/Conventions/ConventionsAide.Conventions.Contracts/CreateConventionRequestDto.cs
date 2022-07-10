using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConventionsAide.Conventions.Contracts
{
    public class CreateConventionRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long? VenuesConfirmationFlowId { get; set; }
        public IEnumerable<long>? TopicIds { get; set; }
        public long? GuestsInvitationFlowId { get; set; }
        public long? TalkersInvitationFlowId { get; set; }
        public DateOnly From { get; set; }
        public DateOnly To { get; set; }
    }
}
