using Domain.Entities.RoomEntities;
using Domain.Enums;

namespace Domain.Entities.CardEntities
{
    public class Stack
    {
        public int Id { get; set; }
        public Room Room { get; set; }
        public string RoomId { get; set; }
        public List<Card> Cards { get; set; } = new List<Card>();
        public StackDrawingMode Mode { get; set; } = StackDrawingMode.All;

    }
}
