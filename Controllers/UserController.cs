using System.Runtime.CompilerServices;
using Bourt.DTOs.Request.User;
using Bourt.DTOs.Response;
using Bourt.Enums;
using Bourt.Services.Implementation;
using Bourt.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bourt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _services;
        
        public UserController(IUserServices services)
        {
            _services = services;
        }

        [HttpGet("user-get")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> Get([FromQuery] UserGetRequestModel model, CancellationToken cancellationToken)
        {
            var result = await _services.Get(model, cancellationToken);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("user-register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequestModel model, CancellationToken cancellationToken)
        {
            var result = await _services.Register(model, cancellationToken);

            if (result.Message.ToLower() == "success")
            {
                return Ok(result);  
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("user-login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestModel model, CancellationToken cancellation)
        {
            var result = await _services.Login(model, cancellation);

            if(result.Message.ToLower() == "success")
            {
                return Ok(result.Token);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPatch("user-change-role")]
        public async Task<IActionResult> ChangeRole( [FromBody] UserChangeRoleRequestModel model, CancellationToken cancellationToken)
        {
            var result = await _services.ChangeRole(model, cancellationToken);

            if (result.Message.ToLower() == "success")
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("cek-rahasia")]
        [Authorize] // <--- Gemboknya di sini!
        public IActionResult CekRahasia()
        {
            return Ok("Selamat! Kamu berhasil masuk ke area rahasia dengan Token.");
        }
    }
}
