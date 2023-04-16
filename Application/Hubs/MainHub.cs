using Application.Exceptions;
using Application.Interfaces.Services;
using Application.Services;
using AutoMapper;
using Domain.Entities.RoomEntities;
using Domain.EntityInterfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;


namespace Application.Hubs
{
    public class MainHub : Hub
    {
        private readonly IRoomService _roomService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly ILobbyCounter _counter;
        public MainHub(IRoomService roomService, IMapper mapper, ILobbyCounter counter, IAccountService accountService)
        {
            _roomService = roomService;
            _mapper = mapper;
            _counter = counter;
            _accountService = accountService;
        }

        public override async Task OnConnectedAsync()
        {

            var player = await _accountService.GetPlayer();

            player.ConnectionId = Context.ConnectionId;

            _counter.IncreasePlayersList(player);

            await Clients.All.SendAsync("UpdatePlayersCount", _counter.ShowPlayersCount());

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {

            var playerId = Context.ConnectionId;
            var player = _counter.GetPlayers().FirstOrDefault(p => p.ConnectionId == playerId);

            _counter.DecreasePlayersList(player);

            await Clients.All.SendAsync("UpdatePlayersCount", _counter.ShowPlayersCount());

            await base.OnDisconnectedAsync(exception);
        }

        public async Task<int> OnlinePlayersCount()
        {
            var playersOnlineCount = _counter.ShowPlayersCount();

            await Clients.All.SendAsync("PlayersOnline", playersOnlineCount);

            return playersOnlineCount;
        }

        // Room actions
        [Authorize]
        public async Task<Room> CreateRoom()
        {
            var player = _counter.GetPlayer(Context.ConnectionId);

            if (_counter.GetRooms().Any(r => r.Players.Contains(player)))
            {
                await Clients.Caller.SendAsync("AlreadyInGame");
                throw new BadRequestException("Already in lobby.");
            }
            var gameRoom = _roomService.CreateRoom(player);

            _counter.IncreaseRoomsList(gameRoom);

            await Groups.AddToGroupAsync(player.ConnectionId, gameRoom.RoomId);

            await Clients.Group(gameRoom.RoomId).SendAsync("RoomCreated", gameRoom);

            return gameRoom;
        }

        public async Task<Numbers> setNumberOfRounds(Numbers numberOfRounds)
        {
            var player = await _accountService.GetPlayer();

            var gameRoom = _counter.GetRooms().FirstOrDefault(r => r.RoomAdmin == player);


            if (gameRoom is null)
            {
                throw new BadRequestException("Room not exist.");
            }

            if (player.Role != Roles.RoomAdmin)
            {
                throw new Exceptions.UnauthorizedAccessException("Only RoomAdmin can choose number of rounds.");
            }

            gameRoom.NumberOfRounds = numberOfRounds;

            await Clients.Group(gameRoom.RoomId).SendAsync("NumberOfRoundSet", gameRoom);

            return gameRoom.NumberOfRounds;
        }

        public async Task<Room> JoinRoomById(string roomId)
        {
            var player = await _accountService.GetPlayer();
            var gameRoom = _counter.GetRooms().First(r => r.RoomId == roomId);

            if (gameRoom is null)
            {
                throw new BadRequestException($"Room {roomId} not exist.");
            }

            gameRoom.Players.Add(player);

            await Clients.Group(gameRoom.RoomId).SendAsync("PlayerJoined", player);
            await Groups.AddToGroupAsync(player.ConnectionId, gameRoom.RoomId);

            return gameRoom;
        }

    }
}
