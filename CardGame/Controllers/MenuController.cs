using Application.DTO;
using Application.Exceptions;
using Application.Interfaces.Services;
using Domain.Entities.PlayerEntities;
using Domain.Entities.RoomEntities;
using Microsoft.AspNetCore.Mvc;

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

            var gameRoom = await _roomService.CreateRoom();

            if (gameRoom is null)
            {
                throw new BadRequestException("Something went wrong.");
            }

            return gameRoom;
        }

        [HttpGet("GetRoomInfo")]
        public async Task<ActionResult> GetRoomInfo([FromBody] RoomRequestDTO request)
        {
            var room = await _roomService.GetRoomInfo(request.RoomId);

            if (room == null)
            {
                return BadRequest("Invalid operation");
            }

            return Ok(room);
        }
    }
}
