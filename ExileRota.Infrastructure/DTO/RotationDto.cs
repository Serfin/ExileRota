using System;
using System.Collections.Generic;

namespace ExileRota.Infrastructure.DTO
{
    public class RotationDto
    {
        public Guid RotationId { get; set; }
        public Guid Creator { get; set; }
        public string League { get; set; }
        public string Type { get; set; }
        public int? Spots { get; set; }
        public IEnumerable<UserDto> Members { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}