using ConventionsAide.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConventionsAide.Conventions.Contracts
{
    public class UpdateConventionTalkWebRequestDto
    {
        public long? TalkOrderId { get; set; }

        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
