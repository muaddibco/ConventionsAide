using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConventionsAide.VenueOrders.Domain.Shared
{
    public enum VenueOrderStatus
    {
        New = 1,
        InProgress,
        Confirmed,
        Rejected,
        Cancelled
    }
}
