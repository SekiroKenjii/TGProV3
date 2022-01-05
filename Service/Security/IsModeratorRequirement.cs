using Data;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Service.Security
{
    public class IsModeratorRequirement : IAuthorizationRequirement
    {
    }

    public class IsModeratorRequirementHandle : AuthorizationHandler<IsModeratorRequirement>
    {
        private readonly DataContext _dataContext;
        public IsModeratorRequirementHandle(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsModeratorRequirement requirement)
        {
            var claimUserId = context.User.FindFirst(ClaimTypes.NameIdentifier);

            if (claimUserId == null) return Task.CompletedTask;

            var userId = Guid.TryParse(claimUserId.Value, out Guid id) ? id : default;

            if (userId == default) return Task.CompletedTask;

            var roles = _dataContext.UserRoles!.Include(x => x.Role).Where(x => x.UserId == userId).Select(x => x.Role!.Name).ToList();

            if (roles.Contains(Role.Moderator.ToString()) || roles.Contains(Role.Admin.ToString())) context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
