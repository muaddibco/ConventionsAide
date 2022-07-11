using ConventionsAide.Core.Common.Architecture;
using ConventionsAide.Core.Common.DependencyInjection;
using ConventionsAide.Core.EntityFrameworkCore;
using ConventionsAide.Core.EntityFrameworkCore.Repositories;
using ConventionsAide.VenueOrders.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConventionsAide.VenueOrders.EntityFrameworkCore.Repositories
{
    [ScopedService<IVenueOrdersRepository>]
    public class VenueOrdersRepository : EfCoreRepository<VenueOrdersDbContext, VenueOrder, long>, IVenueOrdersRepository
    {
        public VenueOrdersRepository(IDbContextProvider dbContextProvider, ILazyServiceProvider lazyServiceProvider) : base(dbContextProvider, lazyServiceProvider)
        {
        }
    }
}
