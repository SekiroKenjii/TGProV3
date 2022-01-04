using Core.Accessors;
using Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Service;
using Service.Photo;
using Service.Security;
using System.Text;

namespace API.Extensions
{
    public static class AuthenticationServiceExtensions
    {
        public static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration config)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsModerator", policy =>
                {
                    policy.Requirements.Add(new IsModeratorRequirement());
                });
                options.AddPolicy("IsStaff", policy =>
                {
                    policy.Requirements.Add(new IsStaffRequirement());
                });
            });

            services.AddTransient<IAuthorizationHandler, IsModeratorRequirementHandle>();
            services.AddTransient<IAuthorizationHandler, IsStaffRequirementHandle>();

            services.AddHttpContextAccessor();

            services.AddTransient<IUserAccessor, UserAccessor>();
            services.AddTransient<IPhotoAccessor, PhotoAccessor>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}
