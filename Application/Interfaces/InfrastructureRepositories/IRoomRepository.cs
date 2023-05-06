
using Domain.Entities.RoomEntities;

namespace Application.Interfaces.InfrastructureRepositories
{
    public interface IRoomRepository
    {
        Task SaveRoomAsync(Room room);
        Task RemoveRoomAsync(Room room);
        Task UpdateRoomAsync(Room gameRoom);
        Task<List<Room>> GetRoomsAsync();
        Task<Room> GetRoomAsync(string roomId);
    }
}
