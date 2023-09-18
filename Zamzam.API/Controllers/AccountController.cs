using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Zamzam.Application.Features.LogIn.Commands;
using Zamzam.Application.Features.Members.Commands.Create;
using Zamzam.Shared;

namespace Zamzam.API.Controllers
{
    public class AccountController : ApiControllerBase
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
        public async Task<IActionResult> Login(LogInMemberCommand Command)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(Command);
                if (result != null)
                {
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(result),
                        Expiration = result.ValidTo
                    });
                }
            }
            return Unauthorized();
        }
    }
}
