using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Threading.Tasks;
using ExileRota.Core.Domain;
using ExileRota.Core.Repositories;
using ExileRota.Infrastructure.EntityFramework;

namespace ExileRota.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PoeRotaContext _context;

        public UserRepository(PoeRotaContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(Guid userId)
            => await _context.Users.SingleOrDefaultAsync(x => x.UserId == userId);

        public async Task<User> GetByEmailAsync(string email)
            => await _context.Users.SingleOrDefaultAsync(x => x.Email == email);

        public async Task<User> GetByUsernameAsync(string username)
            => await _context.Users.SingleOrDefaultAsync(x => x.Username == username);

        public async Task<User> GetByIgnAsync(string ign)
            => await _context.Users.SingleOrDefaultAsync(x => x.Ign == ign);

        // TODO Add pagination (set the limit of query, add offset)

        public async Task<IEnumerable<User>> GetAllAsync()
            => await _context.Users.ToListAsync();
        //
        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.AddOrUpdate(user);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid userId)
        {
            var user = await GetByIdAsync(userId);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}