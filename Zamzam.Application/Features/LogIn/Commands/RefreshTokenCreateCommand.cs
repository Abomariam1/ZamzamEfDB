using MediatR;
using Microsoft.AspNetCore.Identity;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Application.Security;
using Zamzam.Shared;

namespace Zamzam.Application.Features.LogIn.Commands;
public record RefreshTokenCreateCommand(string Token, string RefreshToken): IRequest<Result<AuthResult>>;
internal class RefreshTokenCreateCommandHandler(IUnitOfWork unitOfWork,
    UserManager<ApplicationUser> userManager,
    Token token): IRequestHandler<RefreshTokenCreateCommand, Result<AuthResult>>
{

    public async Task<Result<AuthResult>> Handle(RefreshTokenCreateCommand request, CancellationToken cancellationToken)
    {
        (bool success, string message) isVerified = await token.IsTokenVerified(tokenRequest: request.Token, refreshTokenRequest: request.RefreshToken);
        if(!isVerified.success)
        {
            return await Result<AuthResult>.FailureAsync(isVerified.message);
        }
        RefreshToken? findtoken = await unitOfWork.Repository<RefreshToken>()
              .GetByNameAsync(x => x.NewToken == request.RefreshToken);
        findtoken.IsUsed = true;
        RefreshToken? storedToken = await unitOfWork.Repository<RefreshToken>().UpdateAsync(findtoken);
        int count = await unitOfWork.Save(cancellationToken);
        ApplicationUser? user = count > 0 ? await userManager.FindByIdAsync(storedToken.UserId)
            : null;
        RefreshToken? refreshToken = await token.GenerateJwtTokenAsync(user ?? new ApplicationUser());
        AuthResult? result = new()
        {
            RefreshToken = refreshToken.NewToken,
            Token = refreshToken.Token
        };
        return await Result<AuthResult>.SuccessAsync(result);

    }
}
