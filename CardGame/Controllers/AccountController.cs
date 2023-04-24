using Application.DTO;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
                return BadRequest(registerUser.ToString());
            }

            return Ok($"Register complete.");
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginUserDTO dto)
        {
            var request = await _service.Login(dto);

            return Ok($"{request}");
        }

        [HttpPost("isOnline")]
        public async Task<ActionResult> IsPlayerOnline(LoginUserDTO dto)
        {
            var request = await _service.isPlayerOnline(dto);
            if (request)
            {
                return BadRequest(request);
            }
            return Ok(request);
        }

        [HttpPost("NameValidate")]
        public async Task<ActionResult> NameValidate([FromBody] RegisterNameRequestDTO dto)
        {
            if (dto.Name == null)
            {
                return BadRequest("Invalid data");
            }

            var request = await _service.IsUserNameTaken(dto.Name);

            if (!request)
            {
                return Ok(dto.Name);
            }

            return BadRequest();
        }
    }
}
