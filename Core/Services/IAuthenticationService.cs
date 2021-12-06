using Core.DTOs.Authentication;
using Core.DTOs.User;

namespace Core.Services
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> Login(LoginDto loginDto);
        Task<AuthenticationResponse> Register(RegisterDto registerDto);
        Task<AuthenticationResponse> RefreshToken();
        Task<UserDto> GetCurrentUser();
    }
}
