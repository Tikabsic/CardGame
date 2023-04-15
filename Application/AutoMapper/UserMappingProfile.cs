﻿using Application.DTO;
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

            CreateMap<IEnumerable<System.Security.Claims.Claim>, Player>()
                .ForMember(p => p.Id, c => c.MapFrom(src => int.Parse(src.FirstOrDefault(x => x.Type == "Id").Value)))
                .ForMember(p => p.Name, c => c.MapFrom(src => src.FirstOrDefault(x => x.Type == "UserScore").Value))
                .ForMember(p => p.UserScore, c => c.MapFrom(src => int.Parse(src.FirstOrDefault(x => x.Type =="UserScore").Value)));


            CreateMap<User, Player>();

            CreateMap<RegisterUserDTO, User>();

            CreateMap<LoginUserDTO, User>();

            CreateMap<RegisterNameRequestDTO, User>();
        }
    }
}
