using Domain.Entities.PlayerEntities;
using Domain.Entities.RoomEntities;
using Domain.Interfaces;

namespace Domain.EntityServices
{
    public class RoomEntityService : IRoomEntityService
    {
        private readonly IStackEntityService _stackService;
        private readonly IDeckEntityService _deckService;

        public RoomEntityService(IStackEntityService stackService, IDeckEntityService deckService)
        {
            _stackService = stackService;
            _deckService = deckService;
        }

        private string RoomIdGenerator()
        {
            var numberOfDigits = 6;
            var chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var rng = new Random();
            var id = new char[numberOfDigits];

            for (int i = 0; i < numberOfDigits; i++)
            {
                id[i] = chars[rng.Next(chars.Length)];
            }

            return new string(id);
        }

        public Room CreateRoom(Player player)
        {
            var room = new Room();

            room.RoomId = RoomIdGenerator();

            room.Players.Add(player);

            player.Role = Enums.Roles.RoomAdmin;

            _deckService.GenerateDeck(room.Deck);
            _deckService.ShuffleDeck(room.Deck);

            _stackService.stackDrawingMode(room.Stack);

            return room;
        }

    }
}
