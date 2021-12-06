using Domain.Entities;

namespace Core.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
        UserLoginToken GenerateRefreshToken(bool remember);
    }
}
