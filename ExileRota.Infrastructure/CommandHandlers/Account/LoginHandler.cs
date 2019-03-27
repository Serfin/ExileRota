using System.Runtime.Caching;
using System.Threading.Tasks;
using ExileRota.Infrastructure.Commands.Account;
using ExileRota.Infrastructure.Extensions;
using ExileRota.Infrastructure.Services;

namespace ExileRota.Infrastructure.CommandHandlers.Account
{
    public class LoginHandler : ICommandHandler<Login>
    {
        private readonly IUserService _userService;
        private readonly IJwtHandler _jwtHandler;
        private readonly MemoryCache _cache;

        public LoginHandler(IUserService userService, IJwtHandler jwtHandler, MemoryCache cache)
        {
            _userService = userService;
            _jwtHandler = jwtHandler;
            _cache = cache;
        }

        public async Task HandleAsync(Login command)
        {
            await _userService.LoginAsync(command.Email, command.Password);
            var user = await _userService.GetByEmailAsync(command.Email);
            var jwt = _jwtHandler.GetToken(user.UserId, user.Role);
            _cache.SetJwt(command.TokenId, jwt);
        }
    }
}