using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExileRota.Infrastructure.DTO;

namespace ExileRota.Infrastructure.Services
{
    public interface IUserService : IService
    {
        Task<UserDto> GetByIdAsync(Guid userId);
        Task<UserDto> GetByEmailAsync(string email);
        Task<UserDto> GetByIgnAsync(string ign);
        Task<UserDto> GetByUsernameAsync(string username);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task RegisterAsync(Guid userId, string username, string password, string email, string ign, string role);
        Task LoginAsync(string email, string password);
        Task RemoveAsync(Guid userId);
    }
}