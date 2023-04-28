using Application.DTO;
using Application.Exceptions;
using Application.Interfaces.InfrastructureRepositories;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.PlayerEntities;
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
        private readonly IMapper _mapper;
        private readonly IMessageRepository _messageRepository;

        public Room GameRoom { get; set; }

        public GameRoomHub(IRoomRepository roomRepository,
                 IAccountService accountService,
                 IPlayerRepository playerRepository,
                 IRoomService roomService,
                 IMapper mapper,
                 IMessageRepository messageRepository)
        {
            _roomRepository = roomRepository;
            _accountService = accountService;
            _playerRepository = playerRepository;
            _roomService = roomService;
            _mapper = mapper;
            _messageRepository = messageRepository;
        }



        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var playerId = Context.ConnectionId;

            var playersList = await _playerRepository.GetPlayersAsync();

            var player = playersList.FirstOrDefault(p => p.ConnectionId == playerId);

            var room = await _roomRepository.GetRoomAsync(player.GameRoomId);

            room.Players.Remove(player);

            await Groups.RemoveFromGroupAsync(room.RoomId, playerId);

            await Clients.Groups(room.RoomId).SendAsync("PlayerLeft", player.Name);

            await _playerRepository.RemovePlayerAsync(player);

            await _roomService.SetGameAdminAsync(room.RoomId);

            await _roomRepository.UpdateRoomAsync(room);

            await Clients.Groups(room.RoomId).SendAsync("RoomUpdate", room);

            if (room.Players.Count() < 1)
            {
                await _roomRepository.RemoveRoomAsync(room);
            }
            await Clients.All.SendAsync("UpdatePlayersCount", await _playerRepository.ShowPlayersCount());

            await base.OnDisconnectedAsync(exception);
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
            await Clients.Groups(roomId).SendAsync("RoomUpdate", room);

            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);

            await Clients.Groups(roomId).SendAsync("PlayerJoined", player.Name);

            return room;
        }

        public async Task SendMessage(string playerMessage, string authorName, string roomId)
        {
            var room = await _roomRepository.GetRoomAsync(roomId);
            if (room == null)
            {
                throw new BadRequestException("Room not exist.");
            }

            var players = await _playerRepository.GetPlayersAsync();
            var player = players.First(p => p.Name == authorName);
            if (player == null)
            {
                throw new BadRequestException("Player not exist.");
            }

            var message = new Message()
            {
                Author = player,
                AuthorId = player.Id,
                Room = room,
                RoomId = room.RoomId,
                AuthorName = player.Name,
                PlayerMessage = playerMessage,
            };

            await _messageRepository.SaveMessageAsync(message);

            var messageDTO = _mapper.Map<MessageDTO>(message);

            await Clients.Groups(roomId).SendAsync("ReciveMessage", messageDTO);
        }
    }
}
