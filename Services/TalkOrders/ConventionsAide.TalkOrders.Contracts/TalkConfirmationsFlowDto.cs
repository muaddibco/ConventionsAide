using ConventionsAide.Core.Contracts;
using ConventionsAide.TalkOrders.Domain.Shared;

namespace ConventionsAide.TalkOrders.Contracts
{
    public class TalkConfirmationsFlowDto : EntityDto<long>
    {
        public long ConventionId { get; set; }
        public TalkConfirmationsFlowStatus Status { get; set; }

        public List<TalkOrderDto> TalkOrders { get; set; }
    }
}