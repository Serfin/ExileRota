using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExileRota.Core.Domain;

namespace ExileRota.Core.Repositories
{
    public interface IUserRepository : IRepository
    {
        Task<User> GetByIdAsync(Guid userId);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByIgnAsync(string ign);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task RemoveAsync(Guid userId);
    }
}