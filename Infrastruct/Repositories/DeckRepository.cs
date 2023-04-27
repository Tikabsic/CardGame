using Application.Interfaces.InfrastructureRepositories;
using Domain.Entities.CardEntities;
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

            foreach (var card in cards)
            {
                deck.Cards.Add(card);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
