using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RetoTecnico.Application.Services;
using RetoTecnico.Application.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace RetoTecnico.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoreController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenService _tokenService;

        public CoreController(UserManager<IdentityUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok("Usuario registrado exitosamente");
            }

            return BadRequest(result.Errors);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Unauthorized("Email o contraseña incorrectos.");
            }

            if (await _userManager.IsLockedOutAsync(user))
            {
                return Unauthorized("Cuenta bloqueada. Intente de nuevo más tarde.");
            }

            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                await _userManager.ResetAccessFailedCountAsync(user);

                var token = _tokenService.CreateToken(user);
                return Ok(new { Response= "Bienvenido", Token = token });
            }
            else
            {
                await _userManager.AccessFailedAsync(user);
                //si falla al 5to intento lanza el mensaje de bloqueo
                if (await _userManager.IsLockedOutAsync(user))
                {
                    return Unauthorized("Cuenta bloqueada. Intente de nuevo más tarde.");
                }

                return Unauthorized("Email o contraseña incorrectos.");
            }
        }
        [HttpGet("tokenValidator")]
        [Authorize]
        public IActionResult Get()
        {
            return Ok(new { message = "Token válido" });
        }
    }
}
