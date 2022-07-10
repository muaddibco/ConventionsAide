using ConventionsAide.Core.Domain.Entities;
using ConventionsAide.TalkOrders.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConventionsAide.TalkOrders.Domain
{
    public class TalkOrder : Entity<long>
    {
        public string Topic { get; set; }
        public string Description { get; set; }
        public long TalkConfirmationsFlowId { get; set; }
        public TalkConfirmationsFlow TalkConfirmationsFlow { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public TalkOrderStatus Status { get; set; }
    }
}
