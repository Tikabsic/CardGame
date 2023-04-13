using Domain.Enums;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.CardEntities
{
    [NotMapped]
    public class Deck
    {
        public ConcurrentStack<Card> Cards { get; set; }
    }
}
