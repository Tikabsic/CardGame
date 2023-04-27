using Application.Interfaces.InfrastructureRepositories;
using Domain.Entities.CardEntities;
using Infrastruct.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastruct.Repositories
{
    internal class CardsRepository : ICardsRepository
    {
        private readonly AppDbContext _dbContext;

        public CardsRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Card>> GetCardsAsync()
        {
            return await _dbContext.Cards.ToListAsync();
        }
    }
}
