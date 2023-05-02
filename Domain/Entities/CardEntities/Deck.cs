using Domain.Entities.RoomEntities;

namespace Domain.Entities.CardEntities
{
    public class Deck
    {
        public int Id { get; set; }
        public Room Room { get; set; }
        public string RoomId { get; set; }
        public List<DeckCard> Cards { get; set; } = new List<DeckCard>();
    }
}
