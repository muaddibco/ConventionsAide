using ConventionsAide.Core.Common;
using System.Threading.Tasks;

namespace ConventionsAide.Core.Communication
{
    public interface IBroadcastHandler<TCommand> : ICommandHandler where TCommand : class 
    {
        Task Handle(TCommand request);
    }
}
