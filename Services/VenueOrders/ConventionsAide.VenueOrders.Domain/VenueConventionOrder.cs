using ConventionsAide.Core.Domain.Entities;

namespace ConventionsAide.VenueOrders.Domain
{
    public class VenueConventionOrder: Entity<long>
    {
        public long ConventionId { get; set; }
    }
}