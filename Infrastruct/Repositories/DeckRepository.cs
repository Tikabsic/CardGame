using Application.Interfaces.InfrastructureRepositories;
using Domain.Entities.CardEntities;
using Domain.Interfaces;
using Infrastruct.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastruct.Repositories
{
    internal class DeckRepository : IDeckRepository
    {
        private readonly AppDbContext _dbContext;

        public DeckRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Deck> UpdateDeckAsync(int deckId)
        {
            var deck = await _dbContext.Decks.FirstAsync(d => d.Id == deckId);
            _dbContext.Decks.Update(deck);
            await _dbContext.SaveChangesAsync();
            return deck;
        }

        public async Task<Deck> GetDeckAsync(string roomId)
        {
            return await _dbContext.Decks.FirstOrDefaultAsync(d => d.RoomId == roomId);
        }

        public async Task GenerateDeckAsync(int deckId)
        {
            var cards = await _dbContext.Cards.ToListAsync();
            var deck = await _dbContext.Decks.FirstAsync(d => d.Id == deckId);

            Random random = new Random();

            Card[] cardArray = cards.ToArray();

            for (int i = cardArray.Length - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);

                Card temp = cardArray[i];
                cardArray[i] = cardArray[j];
                cardArray[j] = temp;
            }

            foreach (var card in cardArray)
            {
                deck.Cards.Add(new DeckCard()
                {
                    Card = card,
                    CardId = card.Id,
                    Deck = deck,
                    DeckId = deck.Id
                });
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
