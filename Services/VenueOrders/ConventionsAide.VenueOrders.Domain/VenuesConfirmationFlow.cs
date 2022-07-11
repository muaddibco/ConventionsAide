using ConventionsAide.Core.Domain.Entities;
using ConventionsAide.VenueOrders.Domain.Shared;

namespace ConventionsAide.VenueOrders.Domain
{
    public class VenuesConfirmationFlow: Entity<long>
    {
        public long ConventionId { get; set; }

        public VenuesConfirmationFlowStatus Status { get; set; }
        public virtual ICollection<VenueOrder> VenueOrders { get; set; }
    }
}