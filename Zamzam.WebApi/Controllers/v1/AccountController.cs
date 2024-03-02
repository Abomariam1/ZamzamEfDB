using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zamzam.Application.DTOs;
using Zamzam.Application.Features.LogIn.Commands;
using Zamzam.Application.Features.Members.Commands.Create;
using Zamzam.Application.Security;
using Zamzam.Shared;

namespace Zamzam.WebApi.Controllers.v1
{
    public class AccountController(IMediator mediator,
        ILogger<AccountController> logger): ApiControllerBase
    {
        //create Account new user

        [HttpPost("register")]
        public async Task<Result<IdentityResult>> Regestration(CreateMemberCommand command) =>
            await mediator.Send(command);

        [HttpPost("login")]
        public async Task<ActionResult<Result<AuthResult>>> Login(LogInMemberCommand command)
        {
            if(!ModelState.IsValid)
            {
                logger.LogError("Error: User {user} can not login", command.UserName);
                return await Result<AuthResult>.FailureAsync("ادخل اسم المسنخدم وكلمة المرور!!!");
            }
            return await mediator.Send(command);

        }

        [HttpPost("refreshToken")]
        public async Task<ActionResult<Result<AuthResult>>> RefreshTokenAsync(RefreshTokenCreateCommand command)
        {
            if(!ModelState.IsValid)
            {
                return await Result<AuthResult>.FailureAsync("error");
            }
            return await mediator.Send(command);
        }

        [Authorize]
        [HttpGet("getuser")]
        public ActionResult<Result<UserDto>> GetUser() => Result<UserDto>.Success();

    }
}
