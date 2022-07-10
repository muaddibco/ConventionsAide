using ConventionsAide.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConventionsAide.Conventions.Domain
{
    public class ConventionTalk : Entity<long>
    {
        public long TalkOrderId { get; set; }

        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
