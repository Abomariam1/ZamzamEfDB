using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Zamzam.Application.Common.Mappings;
using Zamzam.Application.DTOs;
using Zamzam.Shared;

namespace Zamzam.Application.Features.LogIn.Commands
{
    public record LogInMemberCommand: IRequest<Result<UserDto>>, IMapFrom<ApplicationUser>
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }

    internal class LoginMemberCommandHandler: IRequestHandler<LogInMemberCommand, Result<UserDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;

        public LoginMemberCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }
        async Task<Result<UserDto>> IRequestHandler<LogInMemberCommand, Result<UserDto>>.Handle(LogInMemberCommand request, CancellationToken cancellationToken)
        {
            ApplicationUser? user = await _userManager.FindByNameAsync(request.UserName);
            if(user is not null)
            {
                bool found = await _userManager.CheckPasswordAsync(user, request.Password);
                if(found)
                {
                    IList<string>? roles = await _userManager.GetRolesAsync(user);
                    List<Claim>? claims = new()
                        {
                            new(ClaimTypes.Name,user.UserName),
                            new(ClaimTypes.NameIdentifier,user.Id),
                            new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                        };
                    foreach(string role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }
                    SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]));
                    SigningCredentials credentials = new(
                        key,
                        SecurityAlgorithms.HmacSha256
                        );
                    JwtSecurityToken token = new(
                        issuer: _config["Jwt:Validessure"],
                        audience: _config["Jwt:ValidAudiance"],
                        claims: claims,
                        expires: DateTime.UtcNow.AddDays(1),
                        signingCredentials: credentials
                        );
                    JwtSecurityTokenHandler? tokenHandler = new JwtSecurityTokenHandler();
                    UserDto usr = new()
                    {
                        Token = tokenHandler.WriteToken(token),
                        Expiration = token.ValidTo,
                        UserName = user.UserName,
                    };
                    return await Result<UserDto>.SuccessAsync(usr);
                }
            }
            return await Result<UserDto>.FailureAsync("فشل في تسجيل الدخول...");
        }
    }
}
