using System.Threading.Tasks;
using ExileRota.Infrastructure.Commands.Account;
using ExileRota.Infrastructure.Services;

namespace ExileRota.Infrastructure.CommandHandlers.Account
{
    public class LoginHandler : ICommandHandler<Login>
    {
        private readonly IUserService _userService;
        private readonly IJwtHandler _jwtHandler;

        public LoginHandler(IUserService userService, IJwtHandler jwtHandler)
        {
            _userService = userService;
            _jwtHandler = jwtHandler;
        }

        public async Task HandleAsync(Login command)
        {
            await _userService.LoginAsync(command.Email, command.Password);
            var user = await _userService.GetByEmailAsync(command.Email);
            var jwt = _jwtHandler.GetToken(user.UserId, user.Role);
        }
    }
}