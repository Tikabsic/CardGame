using Domain.Entities.PlayerEntities;
using Domain.Interfaces;
using Application.Interfaces.Services;
using Domain.Entities.RoomEntities;
using Application.Interfaces.InfrastructureRepositories;

namespace Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomEntityService _service;
        private readonly IRoomRepository _repository;

        public RoomService(IRoomEntityService service, IRoomRepository roomRepository)
        {
            _service = service;
            _repository= roomRepository;
        }

        public  async Task<Room> CreateRoom(Player player)
        {
            var room = _service.CreateRoom(player);
            
            await _repository.SaveRoomAsync(room);

            return room;
        }
    }
}
