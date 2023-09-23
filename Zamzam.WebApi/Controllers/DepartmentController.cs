using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamzam.Application.Features.Departments.Commands.Create;
using Zamzam.Application.Features.Departments.Commands.Delete;
using Zamzam.Application.Features.Departments.Commands.Update;
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
        public async Task<Result<int>> Create(DepartmentCreateCommand command)
        {
            if (ModelState.IsValid)
            {
                command.CreatedBy = HttpContext.User.Identity.Name;
                return await _mediator.Send(command);
            }
            return await Result<int>.FailureAsync(0, "Fail to create Department...");
        }

        [HttpPut]
        public async Task<Result<int>> Update(DepartmentUpdateCommand command)
        {
            if (ModelState.IsValid)
            {
                command.UpdatedBy = HttpContext.User.Identity.Name;
                return await _mediator.Send(command);
            }
            return await Result<int>.FailureAsync(0, "Fail to update Department...");
        }
        [HttpDelete]
        public async Task<Result<int>> Delete(DepartmentDeleteCommand command)
        {
            if (ModelState.IsValid)
            {
                return await _mediator.Send(command);
            }
            return await Result<int>.FailureAsync(0, "Fail to Delete Department...");
        }
    }
}
