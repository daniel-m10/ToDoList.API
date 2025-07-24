using Microsoft.AspNetCore.Mvc;
using ToDoListApi.Application.DTOs;
using ToDoListApi.Application.Interfaces;

namespace ToDoListApi.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController(IUserService userService, IAuthService authService) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly IAuthService _authService = authService;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserRequest request)
        {
            var userId = await _userService.CreateUserAsync(request);
            return Ok(new { UserId = userId });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var userId = await _authService.LoginAsync(request);
            return Ok(new { UserId = userId });
        }
    }
}
