using Application.Exceptions;
using Application.Hubs;
using Application.Interfaces.Services;
using Domain.Entities;
using Domain.Entities.PlayerEntities;
using Domain.Entities.RoomEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace CardGame.Controllers
{        

    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IRoomService _roomService;

        public MenuController(IRoomService service, IAccountService accountService)
        {
            _roomService = service;
            _accountService = accountService;
        }

        [HttpGet("getinfo")]
        public async Task<Player> GetPlayerAsync()
        {

            var player = await _accountService.GetPlayer();
            if (player == null)
            {
                return null;
            }
            return player;
        }

        [HttpPost("CreateRoom")]
        public async Task<Room> CreateGameRoom()
        {
            var player = await _accountService.GetPlayer();

            var gameRoom = await _roomService.CreateRoom(player);

            if (gameRoom is null)
            {
                throw new BadRequestException("Something went wrong.");
            }

            return gameRoom;
        }

    }
}
