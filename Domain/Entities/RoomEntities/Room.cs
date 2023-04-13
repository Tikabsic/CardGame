using Domain.Entities.CardEntities;
using Domain.Entities.PlayerEntities;
using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entities.RoomEntities
{
    [NotMapped]
    public class Room
    {
        public string RoomId { get; set; }
        public Player RoomAdmin { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public List<Player> Podium { get; set; } = new List<Player>();
        public List<Message> Chat { get; set; } = new List<Message>();
        public Deck Deck { get; set; }
        public Stack Stack { get; set; }
        public Numbers NumberOfRounds { get; set; }
        public int RoundsPlayed { get; set; } = 0;

        private string RoomIdGenerator()
        {
            var numberOfDigits = 6;
            var chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var rng = new Random();
            var id = new char[numberOfDigits];

            for (int i = 0; i < numberOfDigits; i++)
            {
                id[i] = chars[rng.Next(chars.Length)];
            }

            return new string(id);
        }

        public Room()
        {
            RoomId = RoomIdGenerator();

            Deck = new Deck();
            Stack = new Stack();
        }
    }
}
