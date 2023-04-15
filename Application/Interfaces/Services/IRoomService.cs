using Domain.Entities.PlayerEntities;
using Domain.Entities.RoomEntities;


namespace Application.Interfaces.Services
{
    public interface IRoomService
    {
        Room CreateRoom(Player player);
    }
}
