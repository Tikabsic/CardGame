using Domain.Entities.PlayerEntities;

namespace Application.Interfaces.InfrastructureRepositories
{
    public interface IPlayerRepository
    {
        Task AddPlayerAsync(Player player);
        Task RemovePlayerAsync(Player player);
        Task UpdatePlayerAsync(Player player);
        Task<int> ShowPlayersCount();
        Task<List<Player>> GetPlayersAsync();
        Task<Player> GetPlayerAsync(string connectionId);
    }
}
