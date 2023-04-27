using Application.Interfaces.InfrastructureRepositories;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;


namespace Application.Hubs
{
    public class MainHub : Hub
    {
        private readonly IAccountService _accountService;
        private readonly IPlayerRepository _playerRepository;
        private readonly IRoomRepository _roomRepository;
        public MainHub(IAccountService accountService, IPlayerRepository playerRepository, IRoomRepository roomRepository)
        {
            _accountService = accountService;
            _playerRepository = playerRepository;
            _roomRepository = roomRepository;
        }

        public override async Task OnConnectedAsync()
        {
            var player = await _accountService.GetPlayer();
            player.ConnectionId = Context.ConnectionId;
            var players = await _playerRepository.GetPlayersAsync();

            var existingPlayer = players.Any(p => p.Name == player.Name);
            if (existingPlayer)
            {
                await _playerRepository.UpdatePlayerAsync(player);
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
