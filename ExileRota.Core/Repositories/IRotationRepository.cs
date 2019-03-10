using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExileRota.Core.Domain;

namespace ExileRota.Core.Repositories
{
    public interface IRotationRepository : IRepository
    {
        Task<Rotation> GetAsync(Guid rotationId);
        Task<IEnumerable<Rotation>> GetAsync(string type);
        Task<IEnumerable<Rotation>> GetUserRotations(Guid userId);
        Task<IEnumerable<Rotation>> GetAllAsync();
        Task AddAsync(Rotation rotation);
        Task UpdateAsync(Rotation rotation);
        Task RemoveAsync(Guid rotationId);
    }
}