
using Domain.Entities.CardEntities;
using Domain.Entities.RoomEntities;
using Domain.Enums;

namespace Domain.Entities.PlayerEntities
{
    public class Player
    {
        public int Id { get; set; }
        public Room? GameRoom { get; set; }
        public string? GameRoomId { get; set; }
        public string? ConnectionId { get; set; }
        public bool IsPlayerRound { get; set; } = false;
        public bool IsCardDrewFromDeck { get; set; } = false;
        public bool IsCardThrownToStack { get; set; } = false;
        public bool IsCardsDrewFromStack { get; set; } = false;
        public Roles Role { get; set; } = Roles.Player;
        public string Name { get; set; }
        public List<PlayerCard> Hand { get; set; } = new List<PlayerCard>();
        public List<Message> Messages { get; set; } = new List<Message>();
        public int UserScore { get; set; }
    }
}
