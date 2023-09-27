using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamzam.API.Controllers;
using Zamzam.Application.Features.Employees.Commands.Create;
using Zamzam.Application.Features.Employees.Commands.Delete;
using Zamzam.Application.Features.Employees.Commands.Update;
using Zamzam.Shared;

namespace Zamzam.WebApi.Controllers
{
    [Authorize]
    public class EmployeeController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<Result<int>> Create(EmployeeCreateCommand command)
        {
            if (!ModelState.IsValid)
            {
                return await Result<int>.FailureAsync(0, "Not valid command");
            }
            command.CreatedBy = HttpContext.User.Identity.Name;
            return await _mediator.Send(command);
        }

        [HttpPut]
        public async Task<Result<int>> Update(EmployeeUpdateCommand command)
        {
            if (!ModelState.IsValid)
            {
                return await Result<int>.FailureAsync(0, "Not valid command");
            }
            command.UpdatedBy = HttpContext.User.Identity.Name;
            return await _mediator.Send(command);
        }
        [HttpDelete]
        public async Task<Result<int>> Delete(EmployeeDeleteCommand command)
        {
            if (!ModelState.IsValid)
            {
                return await Result<int>.FailureAsync(0, "Not valid command");
            }
            return await _mediator.Send(command);
        }
    }
}
