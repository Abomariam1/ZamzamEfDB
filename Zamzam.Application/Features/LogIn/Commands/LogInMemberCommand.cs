using MediatR;
using Microsoft.AspNetCore.Identity;
using Zamzam.Application.Common.Mappings;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Application.Security;
using Zamzam.Shared;

namespace Zamzam.Application.Features.LogIn.Commands;

public record LogInMemberCommand: IRequest<Result<AuthResult>>, IMapFrom<ApplicationUser>
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
}

internal class LoginMemberCommandHandler(IUnitOfWork unitOfWork,
    UserManager<ApplicationUser> userManager, Token token): IRequestHandler<LogInMemberCommand, Result<AuthResult>>
{

    async Task<Result<AuthResult>> IRequestHandler<LogInMemberCommand, Result<AuthResult>>
        .Handle(LogInMemberCommand request, CancellationToken cancellationToken)
    {
        ApplicationUser? user = await userManager.FindByNameAsync(request.UserName);
        if(user is not null)
        {
            bool found = await userManager.CheckPasswordAsync(user, request.Password);
            if(found)
            {
                RefreshToken? newToken = await token.GenerateJwtTokenAsync(user);
                RefreshToken? saved = await unitOfWork.Repository<RefreshToken>().AddAsync(newToken);
                int count = await unitOfWork.Save(cancellationToken);
                AuthResult result = new()
                {
                    Token = saved.Token,
                    RefreshToken = saved.NewToken,
                    ExpiredAt = saved.ExpiryDate
                };
                return count > 0 ? Result<AuthResult>.Success(result)
                    : Result<AuthResult>.Failure("فشل في تسجيل الدخول");

            }
        }
        return Result<AuthResult>.Failure("فشل في تسجيل الدخول");
    }
}
