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

            CreateMap<DeckCard, Card>()
                .ForMember(c => c.Suit, dc => dc.MapFrom(src => src.Card.Suit))
                .ForMember(c => c.Value, dc => dc.MapFrom(src => src.Card.Value))
                .ForMember(c => c.Id, dc => dc.MapFrom(src => src.Card.Id));

            CreateMap<StackCard, Card>()
                .ForMember(c => c.Value, sc => sc.MapFrom(src => src.Card.Value))
                .ForMember(c => c.Value, sc => sc.MapFrom(src => src.Card.Value))
                .ForMember(c => c.Id, sc => sc.MapFrom(src => src.Card.Id));

            CreateMap<PlayerCard, Card>()
                .ForMember(c => c.Value, pc => pc.MapFrom(src => src.Card.Value))
                .ForMember(c => c.Value, pc => pc.MapFrom(src => src.Card.Value))
                .ForMember(c => c.Id, pc => pc.MapFrom(src => src.Card.Id));

            CreateMap<Player, PlayerDTO>()
            .ForMember(pd => pd.Hand, p => p.MapFrom(src => src.Hand));

            CreateMap<Room, RoomDTO>()
            .ForMember(rd => rd.Players, r => r.MapFrom(src => src.Players))
            .ForMember(rd => rd.Deck, r => r.MapFrom(src => src.Deck.Cards))
            .ForMember(rd => rd.Stack, r => r.MapFrom(src => src.Stack.Cards));

        }
    }
}
