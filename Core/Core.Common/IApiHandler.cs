using ConventionsAide.Core.Common.Architecture;
using System.Threading.Tasks;

namespace ConventionsAide.Core.Common
{
    [ExtensionPoint]
    public interface IApiHandler
    {
        Task Initialize();
    }

}
