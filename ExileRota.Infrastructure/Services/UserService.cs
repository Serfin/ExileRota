using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ExileRota.Core.Domain;
using ExileRota.Core.Repositories;
using ExileRota.Infrastructure.DTO;

namespace ExileRota.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IEncrypter _encrypter;

        public UserService(IUserRepository userRepository, IMapper mapper,
            IEncrypter encrypter)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _encrypter = encrypter;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetByIdAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
            {
                throw new Exception($"User with id: {userId} does not exist");
            }

            return _mapper.Map<User, UserDto>(user);
        }

        public async Task<UserDto> GetByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null)
            {
                throw new Exception($"User with email: {email} does not exist");
            }

            return _mapper.Map<User, UserDto>(user);
        }

        public async Task<UserDto> GetByUsernameAsync(string username)
        {
            var user = await _userRepository.GetByUsernameAsync(username);

            if (user == null)
            {
                throw new Exception($"User with username: {username} does not exist");
            }

            return _mapper.Map<User, UserDto>(user);
        }

        public async Task<UserDto> GetByIgnAsync(string ign)
        {
            var user = await _userRepository.GetByIgnAsync(ign);

            return _mapper.Map<User, UserDto>(user);
        }

        public async Task RemoveAsync(Guid userId)
        {
            if (userId == null)
            {
                throw new Exception($"User with this id does not exist");
            }

            await _userRepository.RemoveAsync(userId);
        }

        public async Task RegisterAsync(Guid userId, string username, string password,
            string email, string ign, string role)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user != null)
            {
                throw new Exception($"User with email: {email} already exists.");
            }

            user = await _userRepository.GetByUsernameAsync(username);
            if (user != null)
            {
                throw new Exception($"User with username: {username} already exists.");
            }

            user = await _userRepository.GetByIgnAsync(ign);
            if (user != null)
            {
                throw new Exception($"User with username: {ign} already exists.");
            }

            var salt = _encrypter.GetSalt(password);
            var hash = _encrypter.GetHash(password, salt);

            user = new User(userId, email, username, hash, salt, ign, role);
            await _userRepository.AddAsync(user);
        }

        public async Task LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null)
            {
                throw new Exception("Invalid credentials");
            }

            var hash = _encrypter.GetHash(password, user.Salt);

            if (user.Password == hash)
            {
                return;
            }

            throw new Exception("Invalid email or password");
        }
    }
}