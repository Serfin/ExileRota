using System;
using System.Threading.Tasks;
using ExileRota.Infrastructure.Commands.Rotations;
using ExileRota.Infrastructure.Services;

namespace ExileRota.Infrastructure.CommandHandlers.Rotations
{
    public class CreateRotationHandler : ICommandHandler<CreateRotation>
    {
        private readonly IRotationService _rotationService;

        public CreateRotationHandler(IRotationService rotationService)
        {
            _rotationService = rotationService;
        }

        public async Task HandleAsync(CreateRotation command)
        {
            await _rotationService.AddAsync(Guid.NewGuid(), command.UserId,
                command.League, command.Type, command.Spots);
        }
    }
}