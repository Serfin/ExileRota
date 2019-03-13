using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using ExileRota.Core.Domain;
using ExileRota.Core.Repositories;
using ExileRota.Infrastructure.EntityFramework;

namespace ExileRota.Infrastructure.Repositories
{
    public class RotationRepository : IRotationRepository
    {
        private readonly PoeRotaContext _context;

        public RotationRepository(PoeRotaContext context)
        {
            _context = context;
        }

        public async Task<Rotation> GetAsync(Guid rotationId)
            => await _context.Rotations.SingleOrDefaultAsync(x => x.RotationId == rotationId);

        public async Task<IEnumerable<Rotation>> GetAsync(string type)
            => await _context.Rotations.AsQueryable().Where(x => x.Type == type).ToListAsync();

        public async Task<IEnumerable<Rotation>> GetUserRotations(Guid userId)
            => await _context.Rotations.AsQueryable().Where(x => x.Creator == userId).ToListAsync();

        public async Task<IEnumerable<Rotation>> GetAllAsync()
            => await _context.Rotations.AsQueryable().ToListAsync();

        public async Task AddAsync(Rotation rotation)
        {
            _context.Rotations.Add(rotation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Rotation rotation)
        {
            _context.Rotations.AddOrUpdate(rotation);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid rotationId)
        {
            var rotation = await GetAsync(rotationId);
            _context.Rotations.Remove(rotation);
            await _context.SaveChangesAsync();
        }
    }
}