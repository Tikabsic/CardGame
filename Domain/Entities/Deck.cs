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
            // iterujemy po wartościach kart
            for (int i = (int)CardValue.Nine; i <= (int)CardValue.Ace; i++)
            {
                // iterujemy po kolorach
                foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
                {
                    // tworzymy obiekt karty i dodajemy go do talii
                    _cards.Add(new Card { Value = (CardValue)i, Suit = suit });
                }
            }
        }

        public void ShowCards()
        {
            var cards = _cards.ToList();

            foreach (var card in cards)
            {
                Console.WriteLine($"karta : {card.Value}" + $" kolor : {card.Suit}");
            }
        }

        public void ShuffleDeck()
        {
            // Utwórz generator liczb pseudolosowych
            Random random = new Random();

            // Przetasuj karty
            for (int i = _cards.Count - 1; i >= 1; i--)
            {
                // Wylosuj indeks karty, z którą zamienimy aktualną kartę
                int j = random.Next(i + 1);

                // Zamień aktualną kartę z kartą na pozycji j
                Card temp = _cards[i];
                _cards[i] = _cards[j];
                _cards[j] = temp;
            }
        }
    }
}
