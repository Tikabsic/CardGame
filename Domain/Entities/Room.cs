using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [NotMapped]
    public class Room
    {
        public string RoomId { get; private set; }
        public User RoomAdmin { get; set; }
        public List<User> Players { get; set; }
        public Deck Deck { get; set; }
        public Stack Stack { get; set; }
        public GameRules GameRules { get; set; }
        public int Round { get; set; }
    }
}
