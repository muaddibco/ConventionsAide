using ConventionsAide.Core.Domain.Entities;
using ConventionsAide.VenueOrders.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConventionsAide.VenueOrders.Domain
{
    public class VenueOrder : Entity<long>
    {
        public VenuesConfirmationFlow VenuesConfirmationFlow { get; set; }
        public long VenuesConfirmationFlowId { get; set; }
        public long VenueId { get; set; }
        public VenueOrderStatus Status { get; set; }
    }
}
