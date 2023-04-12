using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Domain.Entities.PlayerEntities;
using Domain.EntityInterfaces;
using Microsoft.AspNetCore.Http;
using Application.Interfaces.Services;
using Application.Exceptions;
using Domain.Entities.RoomEntities;

namespace Application.Services
{
    public class RoomService : IRoomService
    {
        private List<Room> _rooms = new List<Room>();
        private readonly IRoomEntityService _service;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public RoomService(IRoomEntityService service, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }


        public async Task<Room> CreateRoom()
        {
            var user = await _httpContextAccessor.HttpContext.AuthenticateAsync();
            if (!user.Succeeded)
            {
                throw new Exceptions.UnauthorizedAccessException("Unauthorized access.");
            }
            var player = _mapper.Map<Player>(user.Principal);

            var room = _service.CreateRoom(player);

            _rooms.Add(room);

            return room;
        }

        public async Task<Room> JoinRoomById(string roomId)
        {
            var user = await _httpContextAccessor.HttpContext.AuthenticateAsync();
            if (!user.Succeeded)
            {
                throw new Exceptions.UnauthorizedAccessException("Unauthorized access.");
            }
            var player = _mapper.Map<Player>(user.Principal);

            var desiredRoom = _rooms.FirstOrDefault(x => x.RoomId == roomId);

            if (desiredRoom == null)
            {
                throw new NotFoundException("Room not found.");
            }

            desiredRoom.Players.Add(player);

            return desiredRoom;
        }

        public List<string> GetRoomsId()
        {
            return _rooms.Select(room => room.RoomId).ToList();
        }

        public List<Room> GetRooms()
        {
            return _rooms.ToList();
        }
    }
}
