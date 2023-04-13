using Domain.Entities.PlayerEntities;
using Domain.Entities.RoomEntities;
using Domain.EntityServices;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace Application.Hubs
{
    public class GameRoomHub : Hub
    {
        private readonly RoomEntityService _roomEntityService;
        private readonly List<Room> _rooms = new List<Room>();
        private readonly List<Player> _players = new List<Player>();
        public GameRoomHub(RoomEntityService roomEntityService, List<Room> rooms)
        {
            _roomEntityService = roomEntityService;
            _rooms = rooms;
        }

        public async Task<Room> CreateRoom(string playerJson)
        {
            var player = JsonConvert.DeserializeObject<Player>(playerJson);

            var gameRoom = _roomEntityService.CreateRoom(player);

            _rooms.Add(gameRoom);

            await Groups.AddToGroupAsync(Context.ConnectionId, gameRoom.RoomId);

            await Clients.Group(gameRoom.RoomId).SendAsync("CreateRoom", gameRoom);

            return gameRoom;
        }
    }
}
