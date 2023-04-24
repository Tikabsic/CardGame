using Application.Interfaces.InfrastructureRepositories;
using Domain.Entities.PlayerEntities;
using Domain.Entities.RoomEntities;
using Infrastruct.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastruct.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly AppDbContext _dbContext;

        public PlayerRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Player> AddPlayerAsync(Player player)
        {
            using var connection = _dbContext.Database.GetDbConnection();
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = "SET IDENTITY_INSERT Players ON";

            await command.ExecuteNonQueryAsync();

            await _dbContext.Players.AddAsync(player);
            await _dbContext.SaveChangesAsync();

            command.CommandText = "SET IDENTITY_INSERT Players OFF";
            await command.ExecuteNonQueryAsync();

            return player;
        }

        public async Task<Player> RemovePlayerAsync(Player player)
        {
            using var connection = _dbContext.Database.GetDbConnection();
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = "SET IDENTITY_INSERT Players ON";

            await command.ExecuteNonQueryAsync();

            _dbContext.Players.Remove(player);
            await _dbContext.SaveChangesAsync();

            command.CommandText = "SET IDENTITY_INSERT Players OFF";
            await command.ExecuteNonQueryAsync();

            return player;
        }

        public async Task<Player> UpdatePlayerAsync(Player player)
        {
            using var connection = _dbContext.Database.GetDbConnection();
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = "SET IDENTITY_INSERT Players ON";

            await command.ExecuteNonQueryAsync();

            _dbContext.Players.Update(player);

            await _dbContext.SaveChangesAsync();

            command.CommandText = "SET IDENTITY_INSERT Players OFF";
            await command.ExecuteNonQueryAsync();

            return player;
        }

        public async Task<List<Player>> GetPlayersAsync()
        {
            return await _dbContext.Players.ToListAsync();
        }

        public async Task<int> ShowPlayersCount()
        {
            return _dbContext.Players.Count();
        }

    }
}
