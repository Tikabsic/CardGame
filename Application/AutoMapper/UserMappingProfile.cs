using Application.DTO;
using AutoMapper;
using Domain.Entities;

namespace Application.AutoMapper
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDTO>();

            CreateMap<User, Player>();

            CreateMap<RegisterUserDTO, User>();
        }
    }
}
