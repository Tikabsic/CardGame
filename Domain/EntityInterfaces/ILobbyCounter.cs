using Domain.Entities.PlayerEntities;
using Domain.Entities.RoomEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EntityInterfaces
{
    public interface ILobbyCounter
    {
        Player GetPlayer(string connectionId);
        Room GetRoom(string roomId);
        List<Room> GetRooms();
        List<Player> GetPlayers();
        void IncreasePlayersList(Player player);
        void DecreasePlayersList(Player player);
        void IncreaseRoomsList(Room room);
        void DecreaseRoomsList(Room room);
        int ShowRoomsCount();
        int ShowPlayersCount();
    }
}
