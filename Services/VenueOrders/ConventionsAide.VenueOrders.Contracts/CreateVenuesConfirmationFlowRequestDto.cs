using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConventionsAide.VenueOrders.Contracts
{
    public class CreateVenuesConfirmationFlowRequestDto
    {
        public long ConventionId { get; set; }
        public List<CreateVenueOrderRequestDto> VenueOrders { get; set; }
    }
}
