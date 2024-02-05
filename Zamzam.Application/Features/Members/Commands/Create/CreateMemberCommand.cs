using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Zamzam.Application.Common.Mappings;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Members.Commands.Create
{
    public record CreateMemberCommand : IRequest<Result<IdentityResult>>, IMapFrom<IdentityUser>
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }

        [Compare("Password")]
        public required string ConfirmPassword { get; set; }
        public string? Email { get; set; }
    }

    internal class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, Result<IdentityResult>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public CreateMemberCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Result<IdentityResult>> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            ApplicationUser user = new()
            {
                UserName = request.UserName,
                Email = request.Email
            };
            IdentityResult result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
                return Result<IdentityResult>.Success(result, "Account Added Successfuly...");
            return Result<IdentityResult>.Failure(result, result.Errors.ToString() ?? "Acoount Creation Faild");


        }
    }
}
