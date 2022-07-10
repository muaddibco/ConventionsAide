using ConventionsAide.Core.Domain.Entities;
using ConventionsAide.Users.Domain.Shared;
using ConventionsAide.VenueOrders.Domain.Shared;

namespace ConventionsAide.Registrations.Domain
{
    public class InvitationsFlow : Entity<long>
    {
        public long ConventionId { get; set; }
        public UserTier? Tier { get; set; }
        public InvitationsFlowStatus Status { get; set; }
    }
}