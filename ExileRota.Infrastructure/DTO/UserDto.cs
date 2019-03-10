using System;

namespace ExileRota.Infrastructure.DTO
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string Ign { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}