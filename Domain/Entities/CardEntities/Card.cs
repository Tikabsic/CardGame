using Domain.Entities.PlayerEntities;
using Domain.Enums;

namespace Domain.Entities.CardEntities
{
    public class Card
    {
        public int Id { get; set; }
        public CardValue Value { get; set; }
        public CardSuit Suit { get; set; }
        public List<DeckCard> DeckCards { get; set; }
        public List<StackCard> StackCards { get; set; }
        public List<PlayerCard> PlayerCards { get; set; }
    }
}
