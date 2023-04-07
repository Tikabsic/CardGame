using Domain.Entities.CardEntities;
using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.PlayerEntities
{
    [NotMapped]
    public class Player
    {
        public int Id { get; set; }
        public Roles Role { get; set; }
        public string Name { get; set; }
        public List<Card> Hand { get; set; } = new List<Card>();
        public int UserScore { get; set; }

    }
}
