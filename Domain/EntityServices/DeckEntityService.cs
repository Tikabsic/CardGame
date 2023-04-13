using Domain.Entities.CardEntities;
using Domain.EntityInterfaces;
using Domain.Enums;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EntityServices
{
    public class DeckEntityService : IDeckEntityService
    {
        public void GenerateDeck(Deck deck)
        {
            var cards = new ConcurrentStack<Card>();

            for (int i = (int)CardValue.Nine; i <= (int)CardValue.Ace; i++)
            {
                foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
                {
                    cards.Push(new Card { Value = (CardValue)i, Suit = suit });
                }
            }

            deck.Cards = cards;
        }

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
                deck.Cards.Push(card);
            }
        }
    }
}
