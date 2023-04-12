using Application.DTO;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.PlayerEntities;
using System.Security.Claims;

namespace Application.AutoMapper
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {

            CreateMap<ClaimsPrincipal, Player>()
                .ForMember(p => p.Id, c => c.MapFrom(src => int.Parse(src.FindFirstValue("Id"))))
                .ForMember(p => p.Name, c => c.MapFrom(src => src.FindFirstValue("Name")))
                .ForMember(p => p.UserScore, c => c.MapFrom(src => int.Parse(src.FindFirstValue("UserScore"))));

            CreateMap<User, Player>();

            CreateMap<RegisterUserDTO, User>();

            CreateMap<LoginUserDTO, User>();

            CreateMap<RegisterNameRequestDTO, User>();
        }
    }
}
