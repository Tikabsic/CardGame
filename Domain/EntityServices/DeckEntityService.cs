using Domain.Entities.CardEntities;
using Domain.Interfaces;

namespace Domain.EntityServices
{
    internal class DeckEntityService : IDeckEntityService
    {

        public void ShuffleDeck(Deck deck)
        {
            Random random = new Random();

            Card[] cardArray = deck.Cards.ToArray();
            deck.Cards.Clear();

            for (int i = cardArray.Length - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);

                Card temp = cardArray[i];
                cardArray[i] = cardArray[j];
                cardArray[j] = temp;
            }

            foreach (var card in cardArray)
            {
                deck.Cards.Add(card);
            }
        }
    }
}
