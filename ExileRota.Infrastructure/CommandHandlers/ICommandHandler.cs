using System.Threading.Tasks;
using ExileRota.Infrastructure.Commands;

namespace ExileRota.Infrastructure.CommandHandlers
{
    public interface ICommandHandler<T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}