using System.Threading.Tasks;
using System.Web.Http;
using ExileRota.Infrastructure.Commands;
using ExileRota.Infrastructure.Commands.Account;

namespace ExileRota.Api.Controllers
{
    public class LoginController : ApiController
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public LoginController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Login([FromBody] Login command)
        {
            await _commandDispatcher.DispatchAsync(command);

            return Ok("Login successful");
        }
    }
}