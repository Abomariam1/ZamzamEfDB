using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamzam.API.Controllers;
using Zamzam.Application.Features.Sales.Commands.Create;
using Zamzam.Shared;

namespace Zamzam.WebApi.Controllers
{
    [Authorize]
    public class SaleOrderController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public SaleOrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<Result<int>> Create(SaleCreateCommand command)
        {
            command.CreatedBy = HttpContext.User.Identity.Name;
            return await _mediator.Send(command);
        }
    }
}
