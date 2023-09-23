using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zamzam.API.Controllers;
using Zamzam.Application.Features.Items.Commands.Create;
using Zamzam.Shared;

namespace Zamzam.WebApi.Controllers
{

    public class ItemController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public ItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<Result<int>> Create(ItemCreateCommand command)
        {
            if (ModelState.IsValid)
            {
                command.CreatedBy = HttpContext.User.Identity.Name;
                return await _mediator.Send(command);
            }
            return Result<int>.Failure(0, "خطاء");
        }
    }
}
