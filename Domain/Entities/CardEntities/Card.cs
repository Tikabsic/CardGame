using Domain.Entities.PlayerEntities;
using Domain.Enums;

namespace Domain.Entities.CardEntities
{
    public class Card
    {
        public int Id { get; set; }
        public CardValue Value { get; set; }
        public CardSuit Suit { get; set; }
        public List<Deck> Decks { get; set; }
        public List<Stack> Stacks { get; set; }
        public List<Player> Players { get; set; }
    }
}
