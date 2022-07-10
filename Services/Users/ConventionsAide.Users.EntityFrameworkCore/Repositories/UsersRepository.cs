using ConventionsAide.Core.Common.Architecture;
using ConventionsAide.Core.Common.DependencyInjection;
using ConventionsAide.Core.EntityFrameworkCore;
using ConventionsAide.Core.EntityFrameworkCore.Repositories;
using ConventionsAide.Users.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConventionsAide.Users.EntityFrameworkCore.Repositories
{
    [ScopedService<IUsersRepository>]
    public class UsersRepository : EfCoreRepository<UsersDbContext, User, long>, IUsersRepository
    {
        public UsersRepository(IDbContextProvider dbContextProvider, ILazyServiceProvider lazyServiceProvider) : base(dbContextProvider, lazyServiceProvider)
        {
        }

        public Task<(List<User>, int)> GetListAsync(int skipCount, int maxResultCount, string sorting, string? filter = null)
        {
            throw new NotImplementedException();
        }
    }
}
