using ConventionsAide.Core.Common.Architecture;
using ConventionsAide.Core.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConventionsAide.VenueOrders.Domain
{
    [ServiceContract]
    public interface IVenueOrdersRepository : IRepository<VenueOrder, long>
    {
    }
}
