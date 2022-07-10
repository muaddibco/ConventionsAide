using ConventionsAide.Core.Contracts;
using ConventionsAide.VenueOrders.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConventionsAideGW.Controllers.VenuesConfirmationFlows
{
    public class UpdateVenuesConfirmationFlowWebRequestDto
    {
        public VenuesConfirmationFlowStatus Status { get; set; }
    }
}
