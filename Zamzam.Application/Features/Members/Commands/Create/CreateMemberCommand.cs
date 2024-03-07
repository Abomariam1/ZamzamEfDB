using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Application.Security;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Members.Commands.Create
{
    public record CreateMemberCommand: IRequest<Result<AuthResult>>
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }

        [Compare("Password")]
        public required string ConfirmPassword { get; set; }
        public string? Email { get; set; }
    }

    internal class CreateMemberCommandHandler(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, Token token): IRequestHandler<CreateMemberCommand, Result<AuthResult>>
    {

        public async Task<Result<AuthResult>> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            ApplicationUser user = new()
            {
                UserName = request.UserName,
                Email = request.Email
            };
            ApplicationUser? existingUser = await userManager.FindByNameAsync(user.UserName);
            if(existingUser != null)
            {
                return await Result<AuthResult>.FailureAsync("هذا الاسم مستخدم من قبل, برجاء استخدام اسم اخر...");
            }
            IdentityResult result = await userManager.CreateAsync(user, request.Password);
            if(result.Succeeded)
            {
                ApplicationUser? newUser = await userManager.FindByNameAsync(request.UserName);
                RefreshToken? newToken = await token.GenerateJwtTokenAsync(newUser);
                RefreshToken? saved = await unitOfWork.Repository<RefreshToken>().AddAsync(newToken);
                int count = await unitOfWork.Save(cancellationToken);
                AuthResult authResult = new()
                {
                    Token = saved.Token,
                    RefreshToken = saved.NewToken,
                    ExpiredAt = saved.ExpiryDate
                };

                return await Result<AuthResult>.SuccessAsync(authResult, "تم تسجيل المستخدم بنجاح....");
            }
            return await Result<AuthResult>.FailureAsync("فشل في تسجيل المستخدم, برجاء المحاولة مرة اخري");


        }
    }
}
