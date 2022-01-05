using Core.DTOs.Authentication;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("/api/[controller]")]
    public class AuthenticationsController : BaseApiController
    {
        private readonly IAuthenticationService _authService;
        public AuthenticationsController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _authService.Login(loginDto);
            return HandleResult(result);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var result = await _authService.Register(registerDto);
            return HandleResult(result);
        }

        [Authorize]
        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var result = await _authService.RefreshToken();
            return HandleResult(result);
        }

        [Authorize]
        [HttpGet]
        [Route("user")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var result = await _authService.GetCurrentUser();
            return HandleResult(result);
        }
    }
}
