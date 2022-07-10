using ConventionsAide.Core.Contracts;
using ConventionsAide.Users.Domain.Shared;
using ConventionsAide.VenueOrders.Domain.Shared;

namespace ConventionsAide.Registrations.Contracts
{
    public class InvitationFlowDto : EntityDto<long>
    {
        public long ConventionId { get; set; }
        public UserTier? Tier { get; set; }
        public InvitationsFlowStatus Status { get; set; }
    }
}