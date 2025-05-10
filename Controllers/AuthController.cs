using Microsoft.AspNetCore.Mvc;
using TechXpress.DTOs;
using TechXpress.Services.Interfaces;

namespace TechXpress.Controllers
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
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            try
            {
                var user = await _authService.RegisterAsync(registerDTO);
                return Ok(new { message = "User registered successfully", user });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Registration Exception: ");
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Registration Exception: {ex.InnerException?.Message}");
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                var token = await _authService.LoginAsync(loginDTO);
                return Ok(new { message = "Login successful", token });
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Login Exception: ");
                Console.WriteLine(ex.Message);
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Login Exception: ");
                Console.WriteLine(ex.Message);
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
