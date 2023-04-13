using Domain.Entities.PlayerEntities;
using Domain.Entities.RoomEntities;
using Domain.EntityInterfaces;

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

        public Room CreateRoom(Player player)
        {
            var room = new Room();

            room.Players.Add(player);
            room.RoomAdmin = player;
            player.Role = Enums.Roles.RoomAdmin;

            _deckService.GenerateDeck(room.Deck);
            _deckService.ShuffleDeck(room.Deck);

            _stackService.stackDrawingMode(room.Stack);

            return room;
        }

    }
}
