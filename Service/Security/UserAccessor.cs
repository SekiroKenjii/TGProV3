using Core.Accessors;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Service.Security
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserEmail()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)!.Value;
        }

        public Guid GetUserId()
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            return Guid.TryParse(userId, out Guid id) ? id : default;
        }
    }
}
