using Core.Helpers;
using Domain.Entities;
using Domain.Settings;

namespace Data
{
    public class SeedData
    {
        public static async Task SeedDefaultCredential(DataContext context, DefaultCredential defaultCredential)
        {
            var adminId = Guid.NewGuid();
            var adminRoleId = Guid.NewGuid();

            if (!context.Roles?.Any() ?? false)
            {
                var roles = new List<Role>
                {
                    new Role
                    {
                        Id = adminRoleId,
                        Name = Core.Enums.Role.Admin.ToString(),
                        Description = Core.Enums.Role.Admin.ToString()
                    },
                    new Role
                    {
                        Name = Core.Enums.Role.Moderator.ToString(),
                        Description = Core.Enums.Role.Moderator.ToString()
                    },
                    new Role
                    {
                        Name = Core.Enums.Role.Staff.ToString(),
                        Description = Core.Enums.Role.Staff.ToString()
                    },
                    new Role
                    {
                        Name = Core.Enums.Role.Basic.ToString(),
                        Description = Core.Enums.Role.Basic.ToString()
                    }
                };

                context.Roles?.AddRange(roles);
                await context.SaveChangesAsync();
            }

            if(!context.Users?.Any() ?? false)
            {
                var passwordHandleResult = defaultCredential.DefaultPassword?.HashPassword();

                var defaultUser = new User
                {
                    Id = adminId,
                    Email = "trungthuongvo109@gmail.com",
                    PhoneNumber = "037 527 4267",
                    FullName = "Võ Trung Thường",
                    Avatar = "https://res.cloudinary.com/dglgzh0aj/image/upload/v1638705794/TGProV3/users/admin_avatar.jpg",
                    AvatarId = "TGProV3/users/admin_avatar",
                    PasswordHash = passwordHandleResult?.PasswordHash,
                    PasswordSalt = passwordHandleResult?.PasswordSalt,
                    IsBlocked = false,
                    Gender = Core.Enums.Gender.Male
                };

                defaultUser.UserRoles?.Add(new UserRole
                {
                    UserId = adminId,
                    RoleId = adminRoleId
                });

                context.Users?.Add(defaultUser);
                await context.SaveChangesAsync();
            }
        }
    }
}
