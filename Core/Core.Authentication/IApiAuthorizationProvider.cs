using ConventionsAide.Core.Common.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConventionsAide.Core.Authentication
{
    [ServiceContract]
    public interface IApiAuthorizationProvider
    {
        Task<string> ObtainTokenAsync(string apiName);
    }
}
