using System;
using System.Threading.Tasks;
using System.Web.Http;
using ExileRota.Infrastructure.Commands;
using ExileRota.Infrastructure.Commands.Account;
using ExileRota.Infrastructure.Extensions;
using System.Runtime.Caching;

namespace ExileRota.Api.Controllers
{
    public class LoginController : ApiController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly MemoryCache _cache;

        public LoginController(ICommandDispatcher commandDispatcher, MemoryCache cache)
        {
            _commandDispatcher = commandDispatcher;
            _cache = cache;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Login([FromBody] Login command)
        {
            command.TokenId = Guid.NewGuid();
            await _commandDispatcher.DispatchAsync(command);
            var jwt = _cache.GetJwt(command.TokenId);

            return Ok(jwt);
        }
    }
}