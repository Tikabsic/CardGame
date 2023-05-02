using Domain.Entities.RoomEntities;
using Domain.Enums;

namespace Domain.Entities.CardEntities
{
    public class Stack
    {
        public int Id { get; set; }
        public Room Room { get; set; }
        public string RoomId { get; set; }
        public List<StackCard> Cards { get; set; } = new List<StackCard>();
        public StackDrawingMode Mode { get; set; } = StackDrawingMode.All;
    }
}
