using System.Threading.Tasks;
using ExileRota.Infrastructure.Commands.Account;
using ExileRota.Infrastructure.Services;

namespace ExileRota.Infrastructure.CommandHandlers.Account
{
    public class DeleteUserHandler : ICommandHandler<DeleteUser>
    {
        private readonly IUserService _userService;
        private readonly IRotationService _rotationService;

        public DeleteUserHandler(IUserService userService, IRotationService rotationService)
        {
            _userService = userService;
            _rotationService = rotationService;
        }

        public async Task HandleAsync(DeleteUser command)
        {
            await _rotationService.DeleteRotationsForUserAsync(command.UserId);
            await _userService.RemoveAsync(command.UserId);
        }
    }
}