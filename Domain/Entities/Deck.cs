using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [NotMapped]
    public class Deck
    {
        public List<Card> _cards = new List<Card>();

        public Deck()
        {
            _cards = new List<Card>();
            
            for (int i = (int)CardValue.Nine; i <= (int)CardValue.Ace; i++)
            {
                
                foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
                {
                    
                    _cards.Add(new Card { Value = (CardValue)i, Suit = suit });
                }
            }
        }

        public List<Card> ShowCards()
        {
            var cards = _cards.ToList();

            return cards;
        }

        public void ShuffleDeck()
        {
            
            Random random = new Random();

            
            for (int i = _cards.Count - 1; i >= 1; i--)
            {
                
                int j = random.Next(i + 1);

                
                Card temp = _cards[i];
                _cards[i] = _cards[j];
                _cards[j] = temp;
            }
        }
    }
}
