using Application.Exceptions;
using Application.Interfaces.InfrastructureRepositories;
using Application.Interfaces.Services;
using Domain.Entities.CardEntities;
using Domain.Entities.RoomEntities;
using Domain.Enums;
using Domain.Interfaces;

namespace Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomEntityService _roomEntityService;
        private readonly IRoomRepository _roomRepository;
        private readonly IDeckRepository _deckRepository;
        private readonly IDeckEntityService _deckEntityService;
        private readonly IPlayerRepository _playerRepository;
        private readonly IStackRepository _stackRepository;
        private readonly IAccountService _accountService;

        public RoomService(IRoomEntityService service,
            IRoomRepository roomRepository,
            IDeckEntityService deckEntityService,
            IDeckRepository deckRepository,
            IPlayerRepository playerRepository,
            IStackRepository stackRepository,
            IAccountService accountService)
        {
            _roomEntityService = service;
            _roomRepository = roomRepository;
            _deckEntityService = deckEntityService;
            _deckRepository = deckRepository;
            _playerRepository = playerRepository;
            _stackRepository = stackRepository;
            _accountService = accountService;
        }

        public async Task<Room> CreateRoom()
        {
            var room = new Room();
            room.RoomId = _roomEntityService.RoomIdGenerator();
            await _roomRepository.SaveRoomAsync(room);

            await _deckRepository.GenerateDeckAsync(room.Deck.Id);
            _deckEntityService.ShuffleDeck(room.Deck);

            return room;
        }

        public async Task<Room> JoinRoomByIdAsync(string roomId)
        {
            var room = await _roomRepository.GetRoomAsync(roomId);
            var player = await _accountService.GetPlayer();

            if (room.Players.Contains(player))
            {
                throw new BadRequestException("Player already in lobby.");
            }

            return room;
        }

        public async Task SetGameAdminAsync(string roomId)
        {
            var room = await _roomRepository.GetRoomAsync(roomId);
            var players = await _playerRepository.GetPlayersAsync();
            var firstPlayer = players.First();

            firstPlayer.Role = Roles.RoomAdmin;

            await _roomRepository.UpdateRoomAsync(room);
        }

        public async Task<Room> GetRoomInfo(string roomId)
        {
            return await _roomRepository.GetRoomAsync(roomId);
        }
    }
}
