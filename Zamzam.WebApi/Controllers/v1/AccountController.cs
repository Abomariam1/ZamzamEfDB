using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Zamzam.Application.Features.LogIn.Commands;
using Zamzam.Application.Features.Members.Commands.Create;
using Zamzam.Shared;

namespace Zamzam.WebApi.Controllers.v1
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
                JwtSecurityToken? result = await _mediator.Send(Command);
                if (result != null)
                {
                    string tkn = "";
                    try
                    {
                        var tok = new JwtSecurityTokenHandler();
                        tkn = tok.WriteToken(result);

                    }
                    catch (Exception e)
                    {
                        var ss = e.Message;
                    }
                    return Ok(new
                    {
                        Token = tkn,
                        Command.UserName,
                        Expiration = result.ValidTo,
                        Refresh_Token = ""
                    });
                }
                return BadRequest("UserName Or Password is incorrect...");
            }
            return Unauthorized();
        }
        [Authorize]
        [HttpGet("getuser")]
        public async Task<IActionResult> GetUser() => Ok();

    }
}
