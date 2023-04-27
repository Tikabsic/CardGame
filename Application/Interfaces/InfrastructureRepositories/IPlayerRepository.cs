using Domain.Entities.PlayerEntities;

namespace Application.Interfaces.InfrastructureRepositories
{
    public interface IPlayerRepository
    {
        Task<Player> AddPlayerAsync(Player player);
        Task<Player> RemovePlayerAsync(Player player);
        Task<Player> UpdatePlayerAsync(Player player);
        Task<int> ShowPlayersCount();
        Task<List<Player>> GetPlayersAsync();
        Task<Player> GetPlayerAsync(string connectionId);
    }
}
