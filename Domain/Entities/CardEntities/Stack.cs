using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Entities.CardEntities
{
    [NotMapped]
    public class Stack
    {
        public ConcurrentBag<Card> Cards { get; set; }
        public StackDrawingMode Mode { get; set; } = StackDrawingMode.All;

    }
}
