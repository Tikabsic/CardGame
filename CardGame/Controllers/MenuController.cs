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

    }
}
