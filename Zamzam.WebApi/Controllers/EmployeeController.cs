using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamzam.API.Controllers;
using Zamzam.Application.DTOs;
using Zamzam.Application.Features.Employees.Commands.Create;
using Zamzam.Application.Features.Employees.Commands.Delete;
using Zamzam.Application.Features.Employees.Commands.Update;
using Zamzam.Application.Features.Employees.Queries;
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
        public async Task<Result<EmployeeDTO>> Create(EmployeeCreateCommand command)
        {
            if (!ModelState.IsValid)
            {
                return await Result<EmployeeDTO>.FailureAsync(null, "Not valid command");
            }
            command.CreatedBy = HttpContext.User.Identity.Name;
            return await _mediator.Send(command);
        }

        [HttpPut]
        public async Task<Result<EmployeeDTO>> Update(EmployeeUpdateCommand command)
        {
            if (!ModelState.IsValid)
            {
                return await Result<EmployeeDTO>.FailureAsync(new EmployeeDTO(), "Not valid command");
            }
            command.UpdatedBy = HttpContext.User.Identity!.Name;
            return await _mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<Result<int>> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return await Result<int>.FailureAsync(0, "Not valid command");
            }
            var command = new EmployeeDeleteCommand(id);
            return await _mediator.Send(command);
        }

        [HttpGet("getall")]
        public async Task<ActionResult<Result<List<EmployeeDTO>>>> GetAllEmployees()
        {
            var emp = await _mediator.Send(new GetAllEmployeesQuery());
            return emp;
        }
    }
}
