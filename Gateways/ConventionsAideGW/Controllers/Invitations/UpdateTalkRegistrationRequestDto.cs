using ConventionsAide.Core.Contracts;
using ConventionsAide.Registrations.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConventionsAide.Registrations.Contracts
{
    public class UpdateTalkRegistrationWebRequestDto
    {
        public TalkRegistrationStatus Status { get; set; }
    }
}
