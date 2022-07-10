using ConventionsAide.Core.Domain.Entities;
using ConventionsAide.TalkOrders.Domain.Shared;

namespace ConventionsAide.TalkOrders.Domain
{
    public class TalkConfirmationsFlow : Entity<long>
    {
        public long ConventionId { get; set; }
        public TalkConfirmationsFlowStatus Status { get; set; }
    }
}