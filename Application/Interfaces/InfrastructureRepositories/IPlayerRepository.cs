using Domain.Entities.PlayerEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.InfrastructureRepositories
{
    public interface IPlayerRepository
    {
        Task<Player> AddPlayerAsync(Player player);
        Task<Player> RemovePlayerAsync(Player player);
        Task<Player> UpdatePlayerAsync(Player player);
        Task<int> ShowPlayersCount();
        Task<List<Player>> GetPlayersAsync();
    }
}
