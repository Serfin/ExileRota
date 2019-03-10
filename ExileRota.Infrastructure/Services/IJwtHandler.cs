using System;
using ExileRota.Infrastructure.DTO;

namespace ExileRota.Infrastructure.Services
{
    public interface IJwtHandler
    {
        JwtDto GetToken(Guid userId, string role);
    }
}