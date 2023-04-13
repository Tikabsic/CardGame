using Domain.Entities.CardEntities;
using Domain.Enums;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Domain.Entities.PlayerEntities
{
    [NotMapped]
    public class Player
    {
        public int Id { get; set; }
        public string ConnectionId { get; set; }
        public Roles Role { get; set; } = Roles.Player;
        public string Name { get; set; }
        public ConcurrentBag<Card> Hand { get; set; } = new ConcurrentBag<Card>();
        public int UserScore { get; set; }
    }
}
