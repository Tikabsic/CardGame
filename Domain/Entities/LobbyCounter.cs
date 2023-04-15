using Domain.Entities.PlayerEntities;
using Domain.Entities.RoomEntities;
using Domain.EntityInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class LobbyCounter : ILobbyCounter
    {
        private List<Player> Players = new List<Player>();
        private List<Room> Rooms = new List<Room>();

        public List<Room> GetRooms()
        {
            return Rooms;
        }

        public List<Player> GetPlayers()
        {
            return Players;
        }

        public void DecreasePlayersList(Player player)
        {
            Players.Remove(player);
        }

        public void DecreaseRoomsList(Room room)
        {
            Rooms.Remove(room);
        }

        public void IncreasePlayersList(Player player)
        {
            Players.Add(player);
        }

        public void IncreaseRoomsList(Room room)
        {
            Rooms.Add(room);
        }

        public int ShowPlayersCount()
        {
            return Players.Count;
        }

        public int ShowRoomsCount()
        {
            return Rooms.Count;
        }
    }
}
