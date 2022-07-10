using ConventionsAide.Core.Contracts;
using ConventionsAide.TalkOrders.Domain.Shared;

namespace ConventionsAide.TalkOrders.Contracts
{
    public class TalkInvitationDto : EntityDto<long>
    {
        public long UserId { get; set; }
        public TalkInvitationStatus Status { get; set; }
    }
}
