using AutoMapper;
using ExileRota.Core.Domain;
using ExileRota.Infrastructure.DTO;

namespace ExileRota.Api
{
    public static class AutomapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<User, UserDto>();
                    cfg.CreateMap<Rotation, RotationDto>();
                })
                .CreateMapper();
    }
}