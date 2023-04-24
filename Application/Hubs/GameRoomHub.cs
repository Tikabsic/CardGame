using Domain.Enums;
using Microsoft.AspNetCore.SignalR;
using Domain.Entities.RoomEntities;
using Application.Interfaces.Services;
using Domain.Interfaces;
using Application.Exceptions;
using Domain.Entities;
using Application.Interfaces.InfrastructureRepositories;

namespace Application.Hubs
{
    public class GameRoomHub : Hub
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IAccountService _accountService;
        private readonly IPlayerRepository _playerRepository;

        public Room GameRoom { get; set; }

        public GameRoomHub(IRoomRepository roomRepository, IAccountService accountService, IPlayerRepository playerRepository)
        {
            _roomRepository = roomRepository;
            _accountService = accountService;
            _playerRepository = playerRepository;
        }


        public async Task<Room> RoomUpdate(string roomId)
        {
            var request = await _roomRepository.GetRoomAsync(roomId);

            await _roomRepository.UpdateRoomAsync(request);

            await Clients.Group(roomId).SendAsync("RoomUpdated", request);

            return request;
        }

        public override async Task OnConnectedAsync()
        {
            var player = await _accountService.GetPlayer();

            player.ConnectionId = Context.ConnectionId;

            await _playerRepository.UpdatePlayerAsync(player);

            var rooms = await _roomRepository.GetRoomsAsync();

            var room = rooms.FirstOrDefault(r => r.Players.Exists(p => p.Name == player.Name));

            await Groups.AddToGroupAsync(room.RoomId, player.ConnectionId);

            await RoomUpdate(room.RoomId);

            await Clients.All.SendAsync("UpdatePlayersCount", await _playerRepository.ShowPlayersCount());

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var players = await _playerRepository.GetPlayersAsync();
            var playerId = Context.ConnectionId;
            var player = players.First(p => p.ConnectionId == playerId);

            var roomsList = await _roomRepository.GetRoomsAsync();

            var isPlayerInGame = roomsList.Any(r => r.Players.Exists(x => x.Name == player.Name));

            if (isPlayerInGame)
            {
                var rooms = await _roomRepository.GetRoomsAsync();
                var room = rooms.First(r => r.Players.Exists(p => p.Name == player.Name));
                await _roomRepository.RemoveRoomAsync(room);
            }

           await _playerRepository.RemovePlayerAsync(player);

            await Clients.All.SendAsync("UpdatePlayersCount", await _playerRepository.ShowPlayersCount());

            await base.OnDisconnectedAsync(exception);
        }

        public async Task<Numbers> setNumberOfRounds(Numbers numberOfRounds)
        {
            var player = await _accountService.GetPlayer();

            var gameRooms = await _roomRepository.GetRoomsAsync();
            var gameRoom  = gameRooms.First(r => r.Players.Contains(player));

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

            await RoomUpdate(gameRoom.RoomId);

            return gameRoom.NumberOfRounds;
        }

    }
}
