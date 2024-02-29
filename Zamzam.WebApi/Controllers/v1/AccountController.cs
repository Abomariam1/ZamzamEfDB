using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Zamzam.Application.DTOs;
using Zamzam.Application.Features.LogIn.Commands;
using Zamzam.Application.Features.Members.Commands.Create;
using Zamzam.Application.Security;
using Zamzam.Shared;

namespace Zamzam.WebApi.Controllers.v1
{
    public class AccountController(IMediator mediator, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, OptionsMonitor<JwtConfig> jwtMonitor, ILogger<AccountController> logger): ApiControllerBase
    {
        private readonly JwtConfig _jwtConfig = jwtMonitor.CurrentValue;

        //create Account new user
        private readonly IMediator _mediator = mediator;

        [HttpPost("register")]
        public async Task<Result<IdentityResult>> Regestration(CreateMemberCommand command) =>
            await _mediator.Send(command);

        [HttpPost("login")]
        public async Task<ActionResult<Result<UserDto>>> Login(LogInMemberCommand Command)
        {
            if(!ModelState.IsValid)
            {
                logger.LogError("Error: User {user} can not login", Command.UserName);
                return await Result<UserDto>.FailureAsync("ادخل اسم المسنخدم وكلمة المرور!!!");
            }
            return await _mediator.Send(Command);

        }

        [Authorize]
        [HttpGet("getuser")]
        public ActionResult<Result<UserDto>> GetUser() => Result<UserDto>.Success();

    }
}
