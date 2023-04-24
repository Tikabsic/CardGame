using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.RoomEntities;
using Domain.Enums;

namespace Domain.Entities.CardEntities
{
    public class Stack
    {
        [NotMapped]
        public List<Card> Cards = new List<Card>();
        public StackDrawingMode Mode { get; set; } = StackDrawingMode.All;

    }
}
