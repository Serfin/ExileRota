using System.Threading.Tasks;
using ExileRota.Infrastructure.Commands.Rotations;
using ExileRota.Infrastructure.Services;

namespace ExileRota.Infrastructure.CommandHandlers.Rotations
{
    public class LeaveRotationHandler : ICommandHandler<LeaveRotation>
    {
        private readonly IRotationService _rotationService;

        public LeaveRotationHandler(IRotationService rotationService)
        {
            _rotationService = rotationService;
        }
        public async Task HandleAsync(LeaveRotation command)
        {
            await _rotationService.DeleteMemberAsync(command.UserId, command.RotationId);
        }
    }
}