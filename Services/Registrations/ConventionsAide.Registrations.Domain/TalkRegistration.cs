using ConventionsAide.Core.Domain.Entities;
using ConventionsAide.Registrations.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConventionsAide.Registrations.Domain
{
    public class TalkRegistration : Entity<long>
    {
        public long InvitationsFlowId { get; set; }
        public InvitationsFlow InvitationsFlow { get; set; }
        public long TalkId { get; set; }
        public long UserId { get; set; }

        public TalkRegistrationStatus Status { get; set; }
    }
}
