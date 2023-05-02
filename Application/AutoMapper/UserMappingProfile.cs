using Application.DTO;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.CardEntities;
using Domain.Entities.PlayerEntities;
using Domain.Entities.RoomEntities;

namespace Application.AutoMapper
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {

            CreateMap<IEnumerable<System.Security.Claims.Claim>, Player>()
                .ForMember(p => p.Name, c => c.MapFrom(src => src.FirstOrDefault(x => x.Type == "Name").Value))
                .ForMember(p => p.UserScore, c => c.MapFrom(src => int.Parse(src.FirstOrDefault(x => x.Type == "UserScore").Value)));


            CreateMap<User, Player>();

            CreateMap<RegisterUserDTO, User>();

            CreateMap<LoginUserDTO, User>();

            CreateMap<RegisterNameRequestDTO, User>();

            CreateMap<MessageDTO, Message>();

            CreateMap<Message, MessageDTO>();

            CreateMap<Card, CardDTO>();

            CreateMap<CardDTO, Card>();

            CreateMap<Room, RoomDTO>()
                .ForMember(rd => rd.Deck, r => r.MapFrom(r => r.Deck.Cards.Find(c => c.Card)));
        }
    }
}
