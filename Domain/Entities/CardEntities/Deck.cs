using Domain.Entities.RoomEntities;

namespace Domain.Entities.CardEntities
{
    public class Deck
    {
        public int Id { get; set; }
        public Room Room { get; set; }
        public string RoomId { get; set; }
        public List<Card> Cards { get; set; } = new List<Card>();

    }
}
