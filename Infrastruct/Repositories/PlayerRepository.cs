using Application.Interfaces.InfrastructureRepositories;
using Domain.Entities.CardEntities;
using Domain.Entities.PlayerEntities;
using Infrastruct.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastruct.Repositories
{
    internal class PlayerRepository : IPlayerRepository
    {
        private readonly AppDbContext _dbContext;

        public PlayerRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task AddPlayerAsync(Player player)
        {
            await _dbContext.Players.AddAsync(player);

            await _dbContext.SaveChangesAsync();
        }

        public async Task RemovePlayerAsync(Player player)
        {
            _dbContext.Players.Remove(player);

            await _dbContext.SaveChangesAsync();

        }

        public async Task UpdatePlayerAsync(Player player)
        {
            _dbContext.Players.Update(player);

            await _dbContext.SaveChangesAsync();

        }

        public async Task<Player> GetPlayerAsync(string connectionId)
        {
            return await _dbContext.Players.FirstAsync(p => p.ConnectionId == connectionId);
        }

        public async Task<List<Player>> GetPlayersAsync()
        {
            return await _dbContext.Players.ToListAsync();
        }

        public async Task<int> ShowPlayersCount()
        {
            return await _dbContext.Players.CountAsync();
        }

    }
}
