using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zamzam.Application.Features.Customers.Commands.Create;
using Zamzam.Application.Features.Customers.Commands.Delete;
using Zamzam.Application.Features.Customers.Commands.Update;
using Zamzam.Application.Features.Customers.Queries.GetAllCustomer;
using Zamzam.Shared;

namespace Zamzam.API.Controllers
{

    public class CustomerController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<Result<List<GetAllCustmerDto>>>> Get() =>
            await _mediator.Send(new GetAllCustomerQuery());
        [HttpPost]
        public async Task<ActionResult<Result<int>>> Create(CreateCustomerCommand command)
        {
            if (!ModelState.IsValid)
            {
                return Result<int>.Failure(0, "خطاء");
            }
            command.CreatedBy = HttpContext.User.Identity.Name;
            return await _mediator.Send(command);
        }

        [HttpPut]
        public async Task<ActionResult<Result<int>>> Update(UpdateCustomerCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("The Model Is not Valid");
            }
            command.UpdatedBy = HttpContext.User.Identity.Name;
            return await _mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<int>>> Delete(int id, DeleteCustomerCommand command) =>
            await _mediator.Send(command);
    }
}
