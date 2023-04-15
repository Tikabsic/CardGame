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
        private readonly IRoomEntityService _service;

        public RoomService(IRoomEntityService service)
        {
            _service = service;
        }

        public  Room CreateRoom(Player player)
        {
            var room = _service.CreateRoom(player);

            return room;
        }
    }
}
