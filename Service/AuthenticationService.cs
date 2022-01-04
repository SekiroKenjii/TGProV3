using AutoMapper;
using Core;
using Core.Accessors;
using Core.Constants;
using Core.DTOs.Authentication;
using Core.DTOs.User;
using Core.Exceptions;
using Core.Helpers;
using Core.Services;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IUserAccessor _userAccessor;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthenticationService(IUnitOfWork unitOfWork, IMapper mapper,
            ITokenService tokenService, IUserAccessor userAccessor, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tokenService = tokenService;
            _userAccessor = userAccessor;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserDto> GetCurrentUser()
        {
            Guid userId = _userAccessor.GetUserId();

            var user = await _unitOfWork.Users.GetByIdAsync(userId);

            if (user == null) throw new NotFoundException(Messages.RESOURCE_NOTFOUND("User"));

            var result = _mapper.Map<UserDto>(user);

            return result;
        }

        public async Task<AuthenticationResponse> Login(LoginDto loginDto)
        {
            var user = await _unitOfWork.Users.GetFirstOrDefaultAsync(x => x.Email == loginDto.Email);

            if (user == null) throw new NotFoundException(Messages.INCORRECT_EMAIL);

            if (user.IsBlocked) throw new UnauthorizedException(Messages.LOCKED_USER);

            var loginResult = PasswordHelper.ValidatePassword(loginDto.Password!, user.PasswordHash!, user.PasswordSalt!);

            if(!loginResult) throw new UnauthorizedException(Messages.INCORRECT_PASSWORD);

            var refreshToken = await CreateRefreshToken(user, loginDto.Remember);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            _httpContextAccessor.HttpContext!.Response.Cookies.Append("refreshToken", refreshToken.Token!, cookieOptions);

            return CreateAuthenticationResponse(user);
        }

        public async Task<AuthenticationResponse> RefreshToken()
        {
            var refreshToken = _httpContextAccessor.HttpContext!.Request.Cookies["refreshToken"];

            Guid userId = _userAccessor.GetUserId();

            var user = await _unitOfWork.Users
                .GetFirstOrDefaultAsync(x => x.Id == userId, new List<string> { "UserLoginTokens" });

            if(user == null) throw new UnauthorizedException(Messages.RESOURCE_NOTFOUND("User"));

            var oldToken = user.UserLoginTokens!.SingleOrDefault(x => x.Token == refreshToken);

            if (oldToken != null && !oldToken.IsActive) throw new UnauthorizedException(Messages.REVOKED_TOKEN);

            return CreateAuthenticationResponse(user);
        }

        public async Task<AuthenticationResponse> Register(RegisterDto registerDto)
        {
            var user = _mapper.Map<User>(registerDto);

            var passwordResponse = registerDto.Password!.HashPassword();

            user.PasswordHash = passwordResponse.PasswordHash;
            user.PasswordSalt = passwordResponse.PasswordSalt;

            user.Gender = (int)registerDto.Gender >= 0 && (int)registerDto.Gender <= 2 ? registerDto.Gender : Gender.Undefined;

            switch (user.Gender)
            {
                case Gender.Female:
                    user.Avatar = Applications.DEFAUlT_FEMALE_AVATAR;
                    user.AvatarId = Applications.DEFAUlT_FEMALE_AVATAR_ID;
                    break;
                default:
                    user.Avatar = Applications.DEFAUlT_MALE_AVATAR;
                    user.AvatarId = Applications.DEFAUlT_MALE_AVATAR_ID;
                    break;
            }

            var basicRole = await _unitOfWork.Roles.GetFirstOrDefaultAsync(x => x.Name == Domain.Enums.Role.Basic.ToString());

            user.UserRoles!.Add(new UserRole
            {
                RoleId = basicRole!.Id
            });

            await _unitOfWork.Users.AddAsync(user);

            var result = await _unitOfWork.SaveChangeAsync() > 0;

            if (!result) throw new BadRequestException();

            var refreshToken = await CreateRefreshToken(user);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            _httpContextAccessor.HttpContext!.Response.Cookies.Append("refreshToken", refreshToken.Token!, cookieOptions);

            return CreateAuthenticationResponse(user);
        }

        private async Task<UserLoginToken> CreateRefreshToken(User user, bool remember = false)
        {
            var refreshToken = _tokenService.GenerateRefreshToken(remember);

            user.UserLoginTokens!.Add(refreshToken);

            _unitOfWork.Users.Update(user!);

            await _unitOfWork.SaveChangeAsync();

            return refreshToken;
        }

        private AuthenticationResponse CreateAuthenticationResponse(User user)
        {
            var jwt = _tokenService.CreateToken(user);

            return new AuthenticationResponse
            {
                Token = jwt
            };
        }
    }
}
