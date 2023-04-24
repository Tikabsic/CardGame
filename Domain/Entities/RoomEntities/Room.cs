using Domain.Entities.CardEntities;
using Domain.Entities.PlayerEntities;
using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entities.RoomEntities
{
    public class Room
    {
        public string RoomId { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public List<Message> Chat { get; set; } = new List<Message>();
        [NotMapped]
        public Deck Deck { get; set; }
        [NotMapped]
        public Stack Stack { get; set; }
        public Numbers NumberOfRounds { get; set; }
        public int RoundsPlayed { get; set; } = 0;

        public Room()
        {
            Deck = new Deck();
            Stack = new Stack();
        }
    }
}
