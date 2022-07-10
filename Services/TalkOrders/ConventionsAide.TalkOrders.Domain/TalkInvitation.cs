using ConventionsAide.Core.Domain.Entities;
using ConventionsAide.TalkOrders.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConventionsAide.TalkOrders.Domain
{
    public class TalkInvitation : Entity<long>
    {
        public long TalkOrderId { get; set; }
        public TalkOrder TalkOrder { get; set; }
        public long UserId { get; set; }
        public TalkInvitationStatus Status { get; set; }
    }
}
