using Application.DTO;
using Application.Exceptions;
using Application.Interfaces.InfrastructureRepositories;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.CardEntities;
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
        private readonly IUserRepository _userRepository;
        private readonly IPlayerService _playerService;

        public Room GameRoom { get; set; }

        public GameRoomHub(IRoomRepository roomRepository,
                 IAccountService accountService,
                 IPlayerRepository playerRepository,
                 IRoomService roomService,
                 IMapper mapper,
                 IMessageRepository messageRepository,
                 IUserRepository userRepository,
                 IPlayerService playerService)
    
        {
            _roomRepository = roomRepository;
            _accountService = accountService;
            _playerRepository = playerRepository;
            _roomService = roomService;
            _mapper = mapper;
            _messageRepository = messageRepository;
            _userRepository = userRepository;
            _playerService = playerService;
        }

        public async Task isPlayerInRoom(string roomId)
        {
            var room = await _roomRepository.GetRoomAsync(roomId);
            var player = await _playerRepository.GetPlayerAsync(Context.ConnectionId);

            var isPlayerInRoom = room.Players.Contains(player);

            if (!isPlayerInRoom)
            {
                string wrongRoomAlert = "Wrong roomId!";
                await Clients.Caller.SendAsync("WrongRoomIdAlert", wrongRoomAlert);
                return;
            }
        }

        public async Task SetGameAdminAsync(string roomId)
        {
            var room = await _roomRepository.GetRoomAsync(roomId);
            var players = await _playerRepository.GetPlayersAsync();
            var firstPlayer = players.FirstOrDefault();

            if (firstPlayer == null)
            {
                return;
            }

            firstPlayer.Role = Roles.RoomAdmin;

            await _roomRepository.UpdateRoomAsync(room);
        }

        public async Task<bool> FinishGame(string roomId)
        {
            var room = await _roomRepository.GetRoomAsync(roomId);
            var caller = await _playerRepository.GetPlayerAsync(Context.ConnectionId);

            var isDeckContainCards = room.Deck.Cards.Any();
            var isPlayerHandsEmpty = caller.Hand.Any();

            if (isDeckContainCards || !isPlayerHandsEmpty)
            {
                return false;
            }
            return true;
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var playerId = Context.ConnectionId;

            var playersList = await _playerRepository.GetPlayersAsync();

            var player = playersList.FirstOrDefault(p => p.ConnectionId == playerId);

            var room = await _roomRepository.GetRoomAsync(player.GameRoomId);

            room.Players.Remove(player);

            await Groups.RemoveFromGroupAsync(room.RoomId, playerId);

            await _playerRepository.RemovePlayerAsync(player);

            await SetGameAdminAsync(room.RoomId);

            await _roomRepository.UpdateRoomAsync(room);

            if (room.Players.Count() == 0)
            {
                await _roomRepository.RemoveRoomAsync(room);
                await base.OnDisconnectedAsync(exception);
                return;
            }

            await Clients.Groups(room.RoomId).SendAsync("PlayerLeft", player.Name);
            await Clients.Groups(room.RoomId).SendAsync("RoomUpdate", room);

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
            await _roomRepository.UpdateRoomAsync(room);
            await SetGameAdminAsync(roomId);

            await Clients.Groups(roomId).SendAsync("RoomUpdate", room);

            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);

            await Clients.Groups(roomId).SendAsync("PlayerJoined", player.Name);

            return room;
        }

        public async Task StartGame(string roomId)
        {
            await isPlayerInRoom(roomId);

            var room = await _roomRepository.GetRoomAsync(roomId);

            if (room.IsGameStarted)
            {
                string alert = "Game already started!";
                await Clients.Caller.SendAsync("GameAlreadyStartedAlert", alert);
                return;
            }

            var caller = await _playerRepository.GetPlayerAsync(Context.ConnectionId);
            if (caller.Role != Roles.RoomAdmin)
            {
                string unauthorizedStartAlert = "You are not room admin.";
                await Clients.Caller.SendAsync("unauthorizedStartAlert", unauthorizedStartAlert);
                return;
            }

            room.IsGameStarted = true;
            room.MaxPlayerRoundIndex = room.Players.Count;
            room.CurrentPlayerRoundIndex = 1;
            await _roomRepository.UpdateRoomAsync(room);

            var currentPlayer = room.Players[room.CurrentPlayerRoundIndex - 1];
            currentPlayer.IsPlayerRound = true;

            await _playerRepository.UpdatePlayerAsync(currentPlayer);

            string message = $"Game started!";

            await Clients.Group(roomId).SendAsync("RoomUpdate", room);
            await Clients.Group(roomId).SendAsync("GameStarted", message);

            await PlayerTurn(roomId);
        }

        public async Task EndTurn(string roomId)
        {
            await isPlayerInRoom(roomId);

            var room = await _roomRepository.GetRoomAsync(roomId);

            if (!room.IsGameStarted)
            {
                string gameNotStartedAlert = "Game not started yet.";
                await Clients.Caller.SendAsync("GameNotStartedAlert", gameNotStartedAlert);
                return;
            }

            var currentPlayer = room.Players[room.CurrentPlayerRoundIndex - 1];
            if (!currentPlayer.IsPlayerRound)
            {
                string alert = "it's not your turn.";
                await Clients.Caller.SendAsync("NotYourTurnAlert", alert);
                return;
            }

            if (room.CurrentPlayerRoundIndex == room.MaxPlayerRoundIndex)
            {
                room.CurrentPlayerRoundIndex = 1;
                currentPlayer.IsPlayerRound = false;
                await _roomRepository.UpdateRoomAsync(room);
                await _playerRepository.UpdatePlayerAsync(currentPlayer);

                var firstPlayer = room.Players[room.CurrentPlayerRoundIndex - 1];
                firstPlayer.IsPlayerRound = true;
                await _playerRepository.UpdatePlayerAsync(firstPlayer);


                await PlayerTurn(roomId);
            }

            if (room.Deck.Cards.Any() && !currentPlayer.IsCardDrewFromDeck)
            {
                var needToDrawAlert = "You must draw a card before finishing the turn!";
                await Clients.Caller.SendAsync("MustDrawAlert", needToDrawAlert);
                return;
            }

            if (await FinishGame(roomId))
            {
                var winner = room.Players.First(p => p.Hand.Count == 0);
                var user = await _userRepository.GetUserByName(winner.Name);
                winner.UserScore++;
                user.UserScore += winner.UserScore;

                await _userRepository.UpdateUser(user);

                var gameFinishMessage = $"Game won by {winner.Name}!";
                await Clients.Group(roomId).SendAsync("GameFinish", gameFinishMessage);
                return;
            }

            room.CurrentPlayerRoundIndex++;
            currentPlayer.IsPlayerRound = false;
            await _roomRepository.UpdateRoomAsync(room);
            await _playerRepository.UpdatePlayerAsync(currentPlayer);

            var nextPlayer = room.Players[room.CurrentPlayerRoundIndex - 1];
            nextPlayer.IsPlayerRound = true;
            await _playerRepository.UpdatePlayerAsync(nextPlayer);

            await PlayerTurn(roomId);
        }

        public async Task PlayerTurn(string roomId)
        {
            await isPlayerInRoom(roomId);

            var room = await _roomRepository.GetRoomAsync(roomId);
            var currentPlayer = room.Players[room.CurrentPlayerRoundIndex - 1];
            if (!currentPlayer.IsPlayerRound)
            {
                string alert = "Something went wrong, it's not your turn.";
                await Clients.Caller.SendAsync("PlayerTurnErrorAlert", alert);
                return;
            }

            string message = $"It's {currentPlayer.Name} turn!.";
            await Clients.Group(roomId).SendAsync("NextTurn", message);
        }

        public async Task DrawACardFromDeck(string roomId)
        {
            await isPlayerInRoom(roomId);

            var room = _roomRepository.GetRoomAsync(roomId);

            var playerDto = await _playerService.DrawACardFromDeck(roomId, Context.ConnectionId);
            var roomDto = _mapper.Map<RoomDTO>(room);

            await Clients.GroupExcept(roomId, Context.ConnectionId).SendAsync("PlayerDrewCardFromDeck", roomDto);
            await Clients.Caller.SendAsync("CardDrawed", playerDto);
        }

        public async Task ThrowACardToStack(string roomId, int cardId)
        {
            await isPlayerInRoom(roomId);
            var caller = await _playerRepository.GetPlayerAsync(Context.ConnectionId);
            var room = await _roomRepository.GetRoomAsync(roomId);

            var roomDto = _mapper.Map<RoomDTO>(room);
            var playerDto = _playerService.ThrowACardToStack(roomId, caller.ConnectionId, cardId);

            await Clients.GroupExcept(roomId, caller.ConnectionId).SendAsync("PlayerThrewCardToStack", roomDto);
            await Clients.Caller.SendAsync("CardThrown", playerDto);
        }

        public async Task DrawACardFromStack(string roomId)
        {
            await isPlayerInRoom(roomId);
            var caller = await _playerRepository.GetPlayerAsync(Context.ConnectionId);
            var room = await _roomRepository.GetRoomAsync(roomId);

            var roomDto = _mapper.Map<RoomDTO>(room);
            var playerDto = _playerService.TakeCardsFromStack(roomId, caller.ConnectionId);

            await Clients.GroupExcept(roomId, caller.ConnectionId).SendAsync("PlayerTookCardsFromStack", roomDto);
            await Clients.Caller.SendAsync("CardsTaken", playerDto);

        }

        public async Task SendMessage(string playerMessage, string authorName, string roomId)
        {
            await isPlayerInRoom(roomId);

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

            if (playerMessage == "deck")
            {
                var cards = room.Deck.Cards.Select(c => c.Card);
                var roomDTO = new RoomDTO();

                var testData = _mapper.Map<RoomDTO>(room);
                await Clients.Caller.SendAsync("DeckCheck", testData);
            };

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
