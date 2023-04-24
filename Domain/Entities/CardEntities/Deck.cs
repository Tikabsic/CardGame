using Domain.Entities.RoomEntities;
using Domain.Enums;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.CardEntities
{
    public class Deck
    {
        [NotMapped]
        public List<Card> Cards { get; set; }
    }
}
