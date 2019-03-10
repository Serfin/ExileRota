using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExileRota.Core.Domain;
using ExileRota.Core.Repositories;

namespace ExileRota.Infrastructure.Repositories
{
    public class InMemoryRotationRepository : IRotationRepository
    {
        private static readonly ISet<Rotation> _rotations = new HashSet<Rotation>();

        public async Task AddAsync(Rotation rotation)
        {
            _rotations.Add(rotation);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Rotation>> GetUserRotations(Guid userId)
            => await Task.FromResult(_rotations.Select(x => x).Where(x => x.Creator == userId));

        public async Task<IEnumerable<Rotation>> GetAllAsync()
            => await Task.FromResult(_rotations);

        public async Task<Rotation> GetAsync(Guid rotationId)
            => await Task.FromResult(_rotations.SingleOrDefault(x => x.RotationId == rotationId));

        public async Task<IEnumerable<Rotation>> GetAsync(string type)
            => await Task.FromResult(_rotations.Select(x => x).Where(x => x.Type == type));

        public async Task RemoveAsync(Guid rotationId)
        {
            var rotation = await GetAsync(rotationId);
            _rotations.Remove(rotation);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Rotation rotation)
        {
            await Task.CompletedTask;
        }
    }
}