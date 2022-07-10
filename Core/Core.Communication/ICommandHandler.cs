using ConventionsAide.Core.Common;
using System.Threading.Tasks;

namespace ConventionsAide.Core.Communication
{
    public interface ICommandHandler<TCommand> : ICommandHandler
        where TCommand : class 
    {
        Task HandleAsync(TCommand request);
    }
}
