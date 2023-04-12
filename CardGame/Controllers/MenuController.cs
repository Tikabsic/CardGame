using Application.Interfaces.Services;
using Domain.Entities.RoomEntities;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("CreateRoom")]
        public async Task<ActionResult> CreateRoom()
        {
            var room = await _roomService.CreateRoom();

            return Ok(room);
        }

        [HttpGet("GetUserInfo")]
        public async Task<ActionResult> GetUserInfo()
        {
            var user = await _accountService.GetUserInfo();

            return Ok(user);
        }

        [HttpPost("JoinRoomById")]
        public async Task<ActionResult> JoinRoomById([FromBody] string roomId)
        {
            var desiredRoom = _roomService.JoinRoomById(roomId);

            return Ok(desiredRoom);
        }

        [HttpGet("GetAllRooms")]
        public async Task<ActionResult> GetAllRooms()
        {
            var rooms = _roomService.GetRooms();
            return Ok(rooms);
        }

        [HttpGet("GetRoomsId")]
        public async Task<ActionResult> GetRoomsId()
        {
            var rooms = _roomService.GetRoomsId();
            return Ok(rooms);
        }
    }
}
