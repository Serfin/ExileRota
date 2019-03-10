using System;
using System.Threading.Tasks;
using System.Web.Http;
using ExileRota.Infrastructure.Commands;
using ExileRota.Infrastructure.Commands.Users;
using ExileRota.Infrastructure.Services;

namespace ExileRota.Api.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUserService _userService;
        private readonly ICommandDispatcher _commandDispatcher;

        public UsersController(IUserService userService, ICommandDispatcher commandDispatcher)
        {
            _userService = userService;
            _commandDispatcher = commandDispatcher;
        }

        //GET api/users
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();

            return Ok(users);
        }

        // GET api/users/email
        [HttpGet]
        [Route("api/users/email/{email}")]
        public async Task<IHttpActionResult> GetByEmail(string email)
        {
            var user = await _userService.GetByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // GET api/users/userId
        [HttpGet]
        [Route("api/users/userid/{userId}")]
        public async Task<IHttpActionResult> GetById(Guid userId)
        {
            var user = await _userService.GetByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // GET api/users/username
        [HttpGet]
        [Route("api/users/username/{username}")]
        public async Task<IHttpActionResult> GetByUsername(string username)
        {
            var user = await _userService.GetByUsernameAsync(username);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // GET api/users/ign
        [HttpGet]
        [Route("api/users/ign/{ign}")]
        public async Task<IHttpActionResult> GetByIgn(string ign)
        {
            var user = await _userService.GetByIgnAsync(ign);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST api/users/CreateUser
        [HttpPost]
        public async Task<IHttpActionResult> Register([FromBody] CreateUser command)
        {
            await _commandDispatcher.DispatchAsync(command);

            return Ok($"users/{command.Email}");
        }
    }
}
