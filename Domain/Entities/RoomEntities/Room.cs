using Domain.Entities.CardEntities;
using Domain.Entities.PlayerEntities;
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
        public Deck Deck { get; set; }
        public Stack Stack { get; set; }
        public RoomRules GameRules { get; set; }
        public int Round { get; set; }

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
            Deck.GenerateDeck();
            Deck.ShuffleDeck();

            Stack = new Stack();       
        }
    }
}
