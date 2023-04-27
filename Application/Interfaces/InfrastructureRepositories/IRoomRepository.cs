
using Domain.Entities.RoomEntities;

namespace Application.Interfaces.InfrastructureRepositories
{
    public interface IRoomRepository
    {
        Task<Room> SaveRoomAsync(Room room);
        Task<Room> RemoveRoomAsync(Room room);
        Task<Room> UpdateRoomAsync(Room gameRoom);
        Task<List<Room>> GetRoomsAsync();
        Task<Room> GetRoomAsync(string roomId);
    }
}
