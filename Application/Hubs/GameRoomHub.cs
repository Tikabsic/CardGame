using Application.Exceptions;
using AutoMapper;
using Domain.Entities.PlayerEntities;
using Domain.Entities.RoomEntities;
using Domain.EntityInterfaces;
using Domain.Enums;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace Application.Hubs
{
    public class GameRoomHub : Hub
    {
        private readonly IRoomEntityService _roomEntityService;
        private readonly IMapper _mapper;
        private readonly ConcurrentBag<Room> _rooms = new ConcurrentBag<Room>();
        private readonly ConcurrentBag<Player> _players = new ConcurrentBag<Player>();
        public GameRoomHub(IRoomEntityService roomEntityService, ConcurrentBag<Room> rooms, IMapper mapper)
        {
            _roomEntityService = roomEntityService;
            _rooms = rooms;
            _mapper = mapper;
        }

        public Player MapPlayer()
        {
            var user = Context.User.Claims;

            var player = _mapper.Map<Player>(user);
            player.ConnectionId = Context.ConnectionId;

            return player;
        }

        [HubMethodName("OnConnectedAsync")]
        public async Task OnConnectedAsync()
        {

            var player = MapPlayer();

            player.ConnectionId = Context.ConnectionId;

            _players.Add(player);

            await Clients.All.SendAsync("UpdatePlayersCount", _players.Count);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {

            var playerId = Context.ConnectionId;
            var player = _players.FirstOrDefault(p => p.ConnectionId == playerId);

            _players.TryTake(out player);

            await Clients.All.SendAsync("UpdatePlayersCount", _players.Count);

            await base.OnDisconnectedAsync(exception);
        }

        public async Task<int> OnlinePlayersCount()
        {
            var playersOnlineCount = _players.Count;

            await Clients.All.SendAsync("PlayersOnline", playersOnlineCount);

            return playersOnlineCount;
        }

        // Room actions
        public async Task<Room> CreateRoom()
        {
            var player = MapPlayer();

            var gameRoom = _roomEntityService.CreateRoom(player);

            _rooms.Add(gameRoom);

            await Groups.AddToGroupAsync(player.ConnectionId, gameRoom.RoomId);

            await Clients.Group(gameRoom.RoomId).SendAsync("RoomCreated", gameRoom);

            return gameRoom;
        }

        public async Task<Numbers> setNumberOfRounds(Numbers numberOfRounds)
        {
            var player = MapPlayer();
            var gameRoom = _rooms.FirstOrDefault(r => r.RoomAdmin == player);

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
            var player = MapPlayer();
            var gameRoom = _rooms.First(r => r.RoomId == roomId);

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
