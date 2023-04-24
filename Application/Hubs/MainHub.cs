using Application.Interfaces.InfrastructureRepositories;
using Application.Interfaces.Services;
using Domain.Entities.RoomEntities;
using Microsoft.AspNetCore.SignalR;


namespace Application.Hubs
{
    public class MainHub : Hub
    {
        private readonly IAccountService _accountService;
        private readonly IPlayerRepository _playerRepository;
        public MainHub(IAccountService accountService, IPlayerRepository playerRepository)
        {
            _accountService = accountService;
            _playerRepository = playerRepository;
        }

        public override async Task OnConnectedAsync()
        {
            var player = await _accountService.GetPlayer();
            var players = await _playerRepository.GetPlayersAsync();
            player.ConnectionId = Context.ConnectionId;

            var existingPlayer = players.FirstOrDefault(p => p.Id == player.Id);
            if (existingPlayer != null)
            {
                existingPlayer.ConnectionId = player.ConnectionId;
                await _playerRepository.UpdatePlayerAsync(existingPlayer);
            }
            else
            {
                await _playerRepository.AddPlayerAsync(player);
            }

            var playersOnline = await _playerRepository.ShowPlayersCount();

            await Clients.All.SendAsync("UpdatePlayersCount", playersOnline);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var playerId = Context.ConnectionId;

            var playersList = await _playerRepository.GetPlayersAsync();

            var player = playersList.FirstOrDefault(p => p.ConnectionId == playerId);

            await _playerRepository.RemovePlayerAsync(player);

            var playersOnline = await _playerRepository.ShowPlayersCount();

            await Clients.All.SendAsync("UpdatePlayersCount", playersOnline);

            await base.OnDisconnectedAsync(exception);
        }

        public async Task<int> OnlinePlayersCount()
        {
            var playersOnlineCount = await _playerRepository.ShowPlayersCount();

            await Clients.All.SendAsync("PlayersOnline", playersOnlineCount);

            return playersOnlineCount;
        }

    }
}
