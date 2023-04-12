using Domain.Entities.PlayerEntities;
using Domain.Entities.RoomEntities;


namespace Application.Interfaces.Services
{
    public interface IRoomService
    {
        Task<Room> CreateRoom();
        List<Room> GetRooms();
        List<string> GetRoomsId();
        Task<Room> JoinRoomById(string roomId);
    }
}
