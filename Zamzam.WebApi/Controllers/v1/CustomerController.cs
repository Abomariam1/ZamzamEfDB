using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamzam.Application.Features.Customers.Commands.Create;
using Zamzam.Application.Features.Customers.Commands.Delete;
using Zamzam.Application.Features.Customers.Commands.Update;
using Zamzam.Application.Features.Customers.Queries.GetAllCustomer;
using Zamzam.Shared;

namespace Zamzam.WebApi.Controllers.v1
{
    [Authorize]
    public class CustomerController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<Result<List<GetAllCustmerDto>>> Get() =>
            await _mediator.Send(new GetAllCustomerQuery());

        [HttpPost]
        public async Task<Result<int>> Create(CreateCustomerCommand command)
        {
            if (!ModelState.IsValid)
            {
                return Result<int>.Failure(0, "خطاء");
            }
            command.CreatedBy = HttpContext.User.Identity.Name;
            return await _mediator.Send(command);
        }

        [HttpPut]
        public async Task<Result<int>> Update(UpdateCustomerCommand command)
        {
            if (!ModelState.IsValid)
            {
                return Result<int>.Failure(0, "خطاء");
            }
            command.UpdatedBy = HttpContext.User.Identity.Name;
            return await _mediator.Send(command);
        }

        [HttpDelete]
        public async Task<Result<int>> Delete(DeleteCustomerCommand command)
        {
            if (!ModelState.IsValid)
            {
                return Result<int>.Failure(0, "خطاء");
            }
            return await _mediator.Send(command);
        }
    }
}
