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
        public async Task<ActionResult<Result<int>>> Create(CreateCustomerCommand command) =>
            await _mediator.Send(command);

        [HttpPut("{id}")]
        public async Task<ActionResult<Result<int>>> Update(int id, UpdateCustomerCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("The Model Is not Valid");
            }
            if (id != command.Id)
            {
                return BadRequest($"Id {command.Id}");
            }
            return await _mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<int>>> Delete(int id, DeleteCustomerCommand command) =>
            await _mediator.Send(command);
    }
}
