using Microsoft.AspNetCore.Mvc;
using VebTechTask.BLL.DTO;
using VebTechTask.BLL.Services.Inrefaces;

namespace VebTechTask.UI.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthService userService, IConfiguration configuration)
        {
            _authService = userService
                ?? throw new ArgumentNullException();
            _configuration = configuration 
                ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(CreateUserDTO userDTO)
        {

            var user = await _authService.Register(userDTO);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, $"{userDTO.Email} could not be register.");
            }

            return StatusCode(StatusCodes.Status200OK, $"A new user has been created: {userDTO.Email}");

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDTO userDTO)
        {
            var secretKey = _configuration["Jwt:SecretKey"];
            var token = await _authService.Authenticate(userDTO.Email, userDTO.Password, secretKey);

            if (!string.IsNullOrEmpty(token))
            {
                return Ok(new { Token = token });
            }
            else
            {
                return Unauthorized(new { error = "Invalid username or password" });
            }
        }
    }
}
