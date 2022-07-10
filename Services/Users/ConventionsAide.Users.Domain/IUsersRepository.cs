using ConventionsAide.Core.Common.Architecture;
using ConventionsAide.Core.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConventionsAide.Users.Domain
{
    [ServiceContract]
    public interface IUsersRepository : IRepository<User, long>
    {
        Task<(List<User>, int)> GetListAsync(int skipCount, int maxResultCount, string sorting, string? filter = null);
    }
}
