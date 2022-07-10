using ConventionsAide.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConventionsAide.VenueOrders.Domain
{
    public class VenueOrder : Entity<long>
    {
        public VenueConventionOrder VenueConventionOrder { get; set; }
        public long VenueConventionOrderId { get; set; }
        public long VenueId { get; set; }

    }
}
