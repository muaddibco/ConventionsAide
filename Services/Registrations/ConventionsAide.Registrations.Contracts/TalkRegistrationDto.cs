using ConventionsAide.Core.Contracts;
using ConventionsAide.Registrations.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConventionsAide.Registrations.Contracts
{
    public class TalkRegistrationDto : EntityDto<long>
    {
        public long InvitationsFlowId { get; set; }
        public long TalkId { get; set; }
        public long UserId { get; set; }

        public TalkRegistrationStatus Status { get; set; }
    }
}
