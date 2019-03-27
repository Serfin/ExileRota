﻿using System.Threading.Tasks;
using System.Web.Http;
using ExileRota.Infrastructure.Commands;
using ExileRota.Infrastructure.Commands.Account;

namespace ExileRota.Api.Controllers
{
    public class AccountController : ApiController
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public AccountController(ICommandDispatcher commmDispatcher)
        {
            _commandDispatcher = commmDispatcher;
        }

        // TODO add admin/moderator authorize
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteUser([FromBody] DeleteUser command)
        {
            await _commandDispatcher.DispatchAsync(command);

            return Ok($"User with id: {command.UserId} and all his rotations has been deleted");
        }
    }
}
