using Application.Exceptions;
using Application.Interfaces.InfrastructureRepositories;
using Application.Interfaces.Services;
using Domain.Entities.RoomEntities;
using Domain.Enums;
using Microsoft.AspNetCore.SignalR;

namespace Application.Hubs
{
    public class GameRoomHub : Hub
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IAccountService _accountService;
        private readonly IPlayerRepository _playerRepository;
        private readonly IRoomService _roomService;

        public Room GameRoom { get; set; }

        public GameRoomHub(IRoomRepository roomRepository, IAccountService accountService, IPlayerRepository playerRepository, IRoomService roomService)
        {
            _roomRepository = roomRepository;
            _accountService = accountService;
            _playerRepository = playerRepository;
            _roomService = roomService;
        }


        public async Task<Room> RoomUpdate(string roomId)
        {
            var request = await _roomRepository.GetRoomAsync(roomId);

            await _roomRepository.UpdateRoomAsync(request);

            await Clients.Group(roomId).SendAsync("RoomUpdated", request);

            return request;
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var playerId = Context.ConnectionId;

            var playersList = await _playerRepository.GetPlayersAsync();

            var player = playersList.FirstOrDefault(p => p.ConnectionId == playerId);

            var room = await _roomRepository.GetRoomAsync(player.GameRoomId);

            room.Players.Remove(player);

            await _playerRepository.RemovePlayerAsync(player);

            await _roomRepository.UpdateRoomAsync(room);

            if (room.Players.Count() < 1)
            {
                await _roomRepository.RemoveRoomAsync(room);
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task<Numbers> setNumberOfRounds(Numbers numberOfRounds)
        {
            var player = await _accountService.GetPlayer();

            var gameRooms = await _roomRepository.GetRoomsAsync();
            var gameRoom = gameRooms.First(r => r.Players.Contains(player));

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

        public async Task<Room> JoinRoomById(string roomId)
        {
            var room = await _roomRepository.GetRoomAsync(roomId);
            var player = await _accountService.GetPlayer();
            player.ConnectionId = Context.ConnectionId;

            room.Players.Add(player);

            player.GameRoom = room;
            player.GameRoomId = roomId;

            await _playerRepository.AddPlayerAsync(player);
            await _roomService.SetGameAdminAsync(roomId);
            await _roomRepository.UpdateRoomAsync(room);

            await Clients.All.SendAsync("UpdatePlayersCount", await _playerRepository.ShowPlayersCount());

            await Groups.AddToGroupAsync(roomId, player.ConnectionId);
            await Clients.Groups(roomId).SendAsync("PlayerJoined", room);

            return room;
        }

    }
}
