using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zamzam.Application.DTOs;
using Zamzam.Application.Features.LogIn.Commands;
using Zamzam.Application.Features.Members.Commands.Create;
using Zamzam.Shared;

namespace Zamzam.WebApi.Controllers.v1
{
    public class AccountController: ApiControllerBase
    {
        //create Account new user
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<Result<IdentityResult>> Regestration(CreateMemberCommand command) =>
            await _mediator.Send(command);

        [HttpPost("login")]
        public async Task<ActionResult<Result<UserDto>>> Login(LogInMemberCommand Command)
        {
            if(!ModelState.IsValid)
            {
                return await Result<UserDto>.FailureAsync("ادخل اسم المسنخدم وكلمة المرور!!!");
            }
            return await _mediator.Send(Command);

        }

        [Authorize]
        [HttpGet("getuser")]
        public IActionResult GetUser() => Ok();

    }
}
