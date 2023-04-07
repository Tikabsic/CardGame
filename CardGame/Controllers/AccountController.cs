using Application.DTO;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Reflection.Metadata.Ecma335;

namespace CardGame.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;
        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUserDTO dto)
        {
            var registerUser = await _service.RegisterUser(dto);

            if (registerUser.Count > 1)
            {
                return BadRequest(registerUser);
            }

            return Ok($"Register complete.");
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUserDTO dto)
        {
            var token = await _service.GenerateJWT(dto);
            return Ok(token);
        }
    }
}
