
using Domain.Entities.CardEntities;
using Domain.Entities.PlayerEntities;
using Domain.Entities;
using Domain.Enums;

namespace Application.DTO
{
    public class RoomDTO
    {
        public string RoomId { get; set; }
        public List<PlayerDTO> Players { get; set; }
        public List<CardDTO> Deck { get; set; }
        public List<CardDTO> Stack { get; set; }
    }
}
