using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExileRota.Core.Domain;
using ExileRota.Core.Repositories;

namespace ExileRota.Infrastructure.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        private static readonly ISet<User> _users = new HashSet<User>();

        public async Task AddAsync(User user)
        {
            _users.Add(user);
            await Task.CompletedTask;
        }

        public async Task<User> GetByUsernameAsync(string username)
            => await Task.FromResult(_users.SingleOrDefault(x => x.Username == username));

        public async Task<User> GetByIgnAsync(string ign)
            => await Task.FromResult(_users.SingleOrDefault(x => x.Ign == ign));

        public async Task<User> GetByIdAsync(Guid userId)
            => await Task.FromResult(_users.SingleOrDefault(x => x.UserId == userId));

        public async Task<User> GetByEmailAsync(string email)
            => await Task.FromResult(_users.SingleOrDefault(x => x.Email == email));

        public async Task<IEnumerable<User>> GetAllAsync()
            => await Task.FromResult(_users);

        public async Task RemoveAsync(Guid userId)
        {
            var user = await GetByIdAsync(userId);
            _users.Remove(user);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(User user)
        {
            await Task.CompletedTask;
        }
    }
}