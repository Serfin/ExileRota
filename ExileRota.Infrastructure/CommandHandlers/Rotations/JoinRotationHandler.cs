using System.Threading.Tasks;
using ExileRota.Infrastructure.Commands.Rotations;
using ExileRota.Infrastructure.Services;

namespace ExileRota.Infrastructure.CommandHandlers.Rotations
{
    public class JoinRotationHandler : ICommandHandler<JoinRotation>
    {
        private readonly IRotationService _rotationService;

        public JoinRotationHandler(IRotationService rotationService)
        {
            _rotationService = rotationService;
        }
        public async Task HandleAsync(JoinRotation command)
        {
            await _rotationService.AddMemberAsync(command.UserId, command.RotationId);
        }
    }
}