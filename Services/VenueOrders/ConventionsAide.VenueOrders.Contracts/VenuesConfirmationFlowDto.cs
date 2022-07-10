using ConventionsAide.Core.Contracts;
using ConventionsAide.VenueOrders.Domain.Shared;

namespace ConventionsAide.VenueOrders.Contracts
{
    public class VenuesConfirmationFlowDto : EntityDto<long>
    {
        public long ConventionId { get; set; }

        public VenuesConfirmationFlowStatus Status { get; set; }

        public List<VenueOrderDto> VenueOrders { get; set; }
    }
}