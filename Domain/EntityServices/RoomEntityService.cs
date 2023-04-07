using Domain.Entities.PlayerEntities;
using Domain.Entities.RoomEntities;
using Domain.EntityInterfaces;

namespace Domain.EntityServices

{
    public class RoomEntityService : IRoomEntityService
    {
        private readonly Room _room;

        public RoomEntityService(Room room)
        {
            _room = room;
        }

        public Room CreateRoom(Player player)
        {
            var room = new Room();

            room.RoomAdmin = player;

            player.Role = Enums.Roles.RoomAdmin;

            room.Players.Add(player);

            return room;
        }
    }
}
