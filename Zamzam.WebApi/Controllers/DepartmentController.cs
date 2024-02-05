using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamzam.Application.DTOs;
using Zamzam.Application.Features.Departments.Commands.Create;
using Zamzam.Application.Features.Departments.Commands.Delete;
using Zamzam.Application.Features.Departments.Commands.Update;
using Zamzam.Application.Features.Departments.Queries;
using Zamzam.Shared;

namespace Zamzam.API.Controllers
{
    [Authorize]
    public class DepartmentController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public DepartmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<Result<DepartmentCreateCommand>> Create(DepartmentCreateCommand command)
        {
            if (!ModelState.IsValid)
            {

                return await Result<DepartmentCreateCommand>.FailureAsync("Fail to create Department...");
            }
            command.CreatedBy = HttpContext.User.Identity!.Name;
            var res = await _mediator.Send(command);
            return res;
        }

        [HttpPut]
        public async Task<Result<int>> Update(DepartmentUpdateCommand command)
        {
            if (ModelState.IsValid)
            {
                command.UpdatedBy = HttpContext.User.Identity!.Name;
                return await _mediator.Send(command);
            }
            return await Result<int>.FailureAsync(0, "Fail to update Department...");
        }
        [HttpDelete("{id}")]
        public async Task<Result<int>> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var command = new DepartmentDeleteCommand { Id = id };
                return await _mediator.Send(command);
            }
            return await Result<int>.FailureAsync(0, "Fail to Delete Department...");
        }

        [HttpGet]
        public async Task<ActionResult<Result<List<DepartmentDTO>>>> Get()
        {
            var result = await _mediator.Send(new GetAllDepartmentsQuery());
            return result;
        }
    }
}
