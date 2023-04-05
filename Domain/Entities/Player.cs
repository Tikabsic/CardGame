using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [NotMapped]
    public class Player
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public Roles Role { get; set; }
        public string Name { get; set; }
        public List<Card> Hand { get; set; } = new List<Card>();
        public int UserScore { get; set; }

    }
}
