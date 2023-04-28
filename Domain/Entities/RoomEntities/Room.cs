using Domain.Entities.CardEntities;
using Domain.Entities.PlayerEntities;
using Domain.Enums;


namespace Domain.Entities.RoomEntities
{
    public class Room
    {
        public string RoomId { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public List<Message> Chat { get; set; } = new List<Message>();
        public Deck Deck { get; set; } = new Deck();
        public Stack Stack { get; set; } = new Stack();
        public int RoundsPlayed { get; set; } = 0;
    }
}
