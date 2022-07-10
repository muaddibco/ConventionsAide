using ConventionsAide.Core.Contracts;
using ConventionsAide.VenueOrders.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConventionsAide.VenueOrders.Contracts
{
    public class UpdateVenueOrderRequestDto : EntityDto<long>
    {
        public VenueOrderStatus Status { get; set; }
    }
}
