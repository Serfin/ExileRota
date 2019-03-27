using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using ExileRota.Infrastructure.Commands;
using ExileRota.Infrastructure.Commands.Rotations;
using ExileRota.Infrastructure.DTO;
using ExileRota.Infrastructure.Services;

namespace ExileRota.Api.Controllers
{
    public class RotationsController : ApiController
    {
        private readonly IRotationService _rotationService;
        private readonly ICommandDispatcher _commandDispatcher;

        public RotationsController(IRotationService rotationService,
            ICommandDispatcher commandDispatcher)
        {
            _rotationService = rotationService;
            _commandDispatcher = commandDispatcher;
        }

        //GET /rotations
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IEnumerable<RotationDto>> GetAll()
            => await _rotationService.GetAllAsync();

        // GET /rotations/type
        [HttpGet]
        [Route("api/rotations/{type}")]
        public async Task<IEnumerable<RotationDto>> Get(string type)
            => await _rotationService.GetByTypeAsync(type);

        // POST /rotations/CreateRotation
        [HttpPost]
        [Route("api/rotations/create")]
        public async Task<IHttpActionResult> CreateRotation([FromBody] CreateRotation command)
        {
            await _commandDispatcher.DispatchAsync(command);

            return Ok($"Created rotation with UserId : {command.UserId}");
        }

        //Post /rotations/JoinRotation
        [HttpPost]
        [Route("api/rotations/join")]
        public async Task<IHttpActionResult> AddMemberToRotation([FromBody] JoinRotation command)
        {
            await _commandDispatcher.DispatchAsync(command);

            return Ok($"Added user : {command.UserId} to rotation : {command.RotationId}");
        }

        [HttpPost]
        [Route("api/rotations/leave")]
        public async Task<IHttpActionResult> RemoveUserFromRotation([FromBody] LeaveRotation command)
        {
            await _commandDispatcher.DispatchAsync(command);

            return Ok($"Removed user : {command.UserId} from rotation : {command.RotationId}");
        }
    }
}