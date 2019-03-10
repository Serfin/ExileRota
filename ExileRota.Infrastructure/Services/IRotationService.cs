using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExileRota.Infrastructure.DTO;

namespace ExileRota.Infrastructure.Services
{
    public interface IRotationService : IService
    {
        Task<IEnumerable<RotationDto>> GetAllAsync();
        Task<IEnumerable<RotationDto>> GetByTypeAsync(string type);
        Task<IEnumerable<RotationDto>> GetUserRotations(Guid userId);
        Task AddAsync(Guid rotationId, Guid user, string league, string type, int spots);
        Task AddMemberAsync(Guid userId, Guid rotationId);
        Task DeleteMemberAsync(Guid userId, Guid rotationId);
        Task DeleteRotationAsync(Guid rotationId);
        Task DeleteRotationsForUserAsync(Guid userId);
    }
}