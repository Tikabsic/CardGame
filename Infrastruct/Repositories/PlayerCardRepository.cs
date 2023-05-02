using Application.Interfaces.InfrastructureRepositories;
using Domain.Entities.CardEntities;
using Infrastruct.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruct.Repositories
{
    internal class PlayerCardRepository : IPlayerCardRepository
    {
        private readonly AppDbContext _dbContext;
        public PlayerCardRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddCard(PlayerCard card)
        {
            await _dbContext.PlayerCards.AddAsync(card);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveCard(PlayerCard card)
        {
            _dbContext.PlayerCards.Remove(card);
            await _dbContext.SaveChangesAsync();
        }
    }
}
