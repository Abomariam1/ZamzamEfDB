using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Zamzam.Application.Common.Mappings;
using Zamzam.Application.Security;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Members.Commands.Create
{
    public record CreateMemberCommand: IRequest<Result<IdentityResult>>, IMapFrom<IdentityUser>
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }

        [Compare("Password")]
        public required string ConfirmPassword { get; set; }
        public string? Email { get; set; }
    }

    internal class CreateMemberCommandHandler: IRequestHandler<CreateMemberCommand, Result<IdentityResult>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateMemberCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<IdentityResult>> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            ApplicationUser user = new()
            {
                UserName = request.UserName,
                Email = request.Email
            };
            ApplicationUser? existingUser = await _userManager.FindByNameAsync(user.UserName);
            if(existingUser != null)
            {
                return Result<IdentityResult>.Failure("هذا الاسم مستخدم من قبل, برجاء استخدام اسم اخر...");
            }
            IdentityResult result = await _userManager.CreateAsync(user, request.Password);
            if(result.Succeeded)
                return Result<IdentityResult>.Success(result, "تم تسجيل المستخدم بنجاح....");
            return Result<IdentityResult>.Failure(result, result.Errors.ToString() ?? "فشل في تسجيل المستخدم, برجاء المحاولة مرة اخري");


        }
    }
}
