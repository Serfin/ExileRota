using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ExileRota.Core.Domain;
using ExileRota.Core.Repositories;
using ExileRota.Infrastructure.DTO;

namespace ExileRota.Infrastructure.Services
{
    public class RotationService : IRotationService
    {
        private readonly IRotationRepository _rotations;
        private readonly IUserService _userService;
        private readonly IUserRepository _users;
        private readonly IMapper _mapper;

        public RotationService(IRotationRepository rotations, IUserService userService, IUserRepository users,
            IMapper mapper)
        {
            _rotations = rotations;
            _userService = userService;
            _users = users;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RotationDto>> GetAllAsync()
        {
            var rotations = await _rotations.GetAllAsync();

            return _mapper.Map<IEnumerable<Rotation>, IEnumerable<RotationDto>>(rotations);
        }

        public async Task<IEnumerable<RotationDto>> GetByTypeAsync(string type)
        {
            var rotations = await _rotations.GetAsync(type);

            return _mapper.Map<IEnumerable<Rotation>, IEnumerable<RotationDto>>(rotations);
        }

        public async Task<IEnumerable<RotationDto>> GetUserRotations(Guid userId)
        {
            var rotations = await _rotations.GetUserRotations(userId);

            return _mapper.Map<IEnumerable<Rotation>, IEnumerable<RotationDto>>(rotations);
        }

        public async Task AddAsync(Guid rotationId, Guid user, string league, string type, int spots)
        {
            var rotation = new Rotation(rotationId, user, league, type, spots);

            await _rotations.AddAsync(rotation);
        }

        public async Task AddMemberAsync(Guid userId, Guid rotationId)
        {
            var user = await _users.GetByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User with this ID does not exist.");
            }

            var rotation = await _rotations.GetAsync(rotationId);
            if (rotation == null)
            {
                throw new Exception("Rotation with this ID does not exist.");
            }

            rotation.AddMember(user);
            await _rotations.UpdateAsync(rotation);
        }

        public async Task DeleteMemberAsync(Guid userId, Guid rotationId)
        {
            var user = await _users.GetByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User with this ID does not exist.");
            }

            var rotation = await _rotations.GetAsync(rotationId);
            if (rotation == null)
            {
                throw new Exception("Rotation with this ID does not exist.");
            }

            rotation.DeleteMember(user);
            await _rotations.UpdateAsync(rotation);
        }

        public async Task DeleteRotationAsync(Guid rotationId)
        {
            var rotation = await _rotations.GetAsync(rotationId);

            if (rotation == null)
            {
                throw new Exception($"Rotation with id: {rotationId} does not exist.");
            }

            await _rotations.RemoveAsync(rotationId);
        }

        public async Task DeleteRotationsForUserAsync(Guid userId)
        {
            var user = await _userService.GetByIdAsync(userId);

            if (user == null)
            {
                throw new Exception($"User with this id does not exist");
            }

            var rotations = await GetUserRotations(userId);

            if (!rotations.Any())
            {
                return;
            }

            foreach (var rotation in rotations)
            {
                await DeleteRotationAsync(rotation.RotationId);
            }
        }
    }
}