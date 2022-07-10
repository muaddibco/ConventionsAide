using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConventionsAide.VenueOrders.Contracts
{
    public class CreateVenueOrderRequestDto
    {
        public long VenuesConfirmationFlowId { get; set; }

        public long VenueId { get; set; }
    }
}
