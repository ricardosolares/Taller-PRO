using Microsoft.AspNetCore.Mvc;
using TallerApi.Models;
using TallerApi.Services;

namespace TallerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(LoginRequestDto request)
        {
            var resultado = await _authService.RegisterAsync(request);

            if (!resultado.Ok)
                return BadRequest(new { mensaje = resultado.Mensaje });

            return Ok(new { mensaje = resultado.Mensaje });
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto request)
        {
            var respuesta = await _authService.LoginAsync(request);

            if (respuesta == null)
                return Unauthorized(new { mensaje = "Usuario o contraseña incorrectos." });

            return Ok(respuesta);
        }
    }
}