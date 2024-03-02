using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Zamzam.Application.Interfaces.Repositories;

namespace Zamzam.Application.Security;
public class Token(IOptionsMonitor<JwtConfig> jwtMonitor,
    TokenValidationParameters tokenValidationParameters,
    IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
{

    private readonly string secretKey = jwtMonitor.CurrentValue.Secret;

    public async Task<RefreshToken> GenerateJwtTokenAsync(ApplicationUser user)
    {
        List<Claim>? claims =
            [
            new Claim("Id",user.Id),
            new(ClaimTypes.Name,user.UserName),
            new(ClaimTypes.NameIdentifier,user.Id),
            new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            ];
        SecurityKey symmetrickey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        SecurityTokenDescriptor? tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddSeconds(10),
            SigningCredentials = new SigningCredentials(symmetrickey, SecurityAlgorithms.HmacSha256),
        };
        RefreshToken usr = null;
        JwtSecurityTokenHandler? tokenHandler = new JwtSecurityTokenHandler();
        try
        {

            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            usr = new()
            {
                JwtId = token.Id,
                AddedDate = DateTime.UtcNow,
                IsRevoked = false,
                IsUsed = false,
                UserId = user.Id,
                ExpiryDate = DateTime.UtcNow.AddMonths(6),
                Token = jwtToken,
                NewToken = $"{RandomString(35)}{Guid.NewGuid().ToString()}",
            };
        }
        catch(Exception ex)
        {
            var msg = ex.Message;
        }
        return usr;
    }



    public async Task<AuthResult> VerifyToken(TokenRequest request, CancellationToken cancellationToken)
    {
        (bool success, string message) isVerified = await IsTokenVerified(tokenRequest: request.Token, refreshTokenRequest: request.RefreshToken);
        if(!isVerified.success)
        {
            return null;
        }
        RefreshToken? findtoken = await unitOfWork.Repository<RefreshToken>()
              .GetByNameAsync(x => x.NewToken == request.RefreshToken);
        findtoken.IsUsed = true;
        RefreshToken? storedToken = await unitOfWork.Repository<RefreshToken>().UpdateAsync(findtoken);
        int count = await unitOfWork.Save(cancellationToken);
        ApplicationUser? user = count > 0 ? await userManager.FindByIdAsync(storedToken.UserId)
            : null;
        RefreshToken? refreshToken = await GenerateJwtTokenAsync(user ?? new ApplicationUser());
        return new AuthResult { RefreshToken = refreshToken.NewToken, Token = refreshToken.Token };

    }


    public async Task<(bool success, string message)> IsTokenVerified(string tokenRequest, string refreshTokenRequest)
    {
        JwtSecurityTokenHandler? jwtTokenHandler = new();
        try
        {
            // validation1
            ClaimsPrincipal? tokenVerification = jwtTokenHandler
                .ValidateToken(tokenRequest, tokenValidationParameters, out var validatedToken);

            // validation2
            if(validatedToken is JwtSecurityToken jwtSecurityToken)
            {
                var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256);
                if(!result)
                    return (false, "");
            }

            // validation3
            long utcExpiryDate = long.Parse(tokenVerification.Claims
                .FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp)!.Value);
            DateTime expiryDate = UnixTimeStampToDate(utcExpiryDate);
            if(expiryDate > DateTime.UtcNow)
                return (false, "Token has not yet expired");

            //validation 4
            RefreshToken? storedToken = await unitOfWork.Repository<RefreshToken>()
                  .GetByNameAsync(x => x.NewToken == refreshTokenRequest);
            if(storedToken == null)
                return (false, "Token is Not Exist");

            // validation 5
            if(storedToken.IsUsed)
                return (false, "Token has been used");

            // validation 6
            if(storedToken.IsRevoked)
                return (false, "Token has been revoked");

            // validate 7
            var jti = tokenVerification.Claims
                .FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)!.Value;
            if(storedToken.JwtId != jti)
                return (false, "Token dose not match");
            return (true, "");
        }
        catch(Exception ex)
        {
            string? message = ex.Message;
            return (false, message);
        }

    }

    private DateTime UnixTimeStampToDate(long utcExpiryDate)
    {
        var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, kind: DateTimeKind.Utc);
        dateTimeVal = dateTimeVal.AddSeconds(utcExpiryDate).ToLocalTime();
        return dateTimeVal;
    }

    private string RandomString(int length)
    {
        var random = new Random();
        var chars = "ABCDEGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(x => x[random.Next(x.Length)]).ToArray());
    }
}
