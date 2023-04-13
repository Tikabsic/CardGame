using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Domain.Entities.PlayerEntities;
using Domain.EntityInterfaces;
using Microsoft.AspNetCore.Http;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;
using Domain.Entities.RoomEntities;
using System.Security.Claims;

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

        public async Task<Room> CreateRoom(Player player)
        {
            var room = _service.CreateRoom(player);

            return room;
        }
    }
}
