using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamzam.API.Controllers;
using Zamzam.Application.Features.Items.Commands.Create;
using Zamzam.Application.Features.Items.Commands.Delete;
using Zamzam.Application.Features.Items.Commands.Update;
using Zamzam.Shared;

namespace Zamzam.WebApi.Controllers
{
    [Authorize]
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
            if (!ModelState.IsValid)
            {
                return Result<int>.Failure(0, "خطاء");
            }
            command.CreatedBy = HttpContext.User.Identity.Name;
            return await _mediator.Send(command);
        }
        [HttpPut]
        public async Task<Result<int>> Update(ItemUpdateCommand command)
        {
            if (!ModelState.IsValid)
            {
                return Result<int>.Failure(0, "خطاء");
            }
            command.UpdatedBy = HttpContext.User.Identity.Name;
            return await _mediator.Send(command);
        }

        [HttpDelete]
        public async Task<Result<int>> Delete(ItemDeleteCommand command)
        {
            if (!ModelState.IsValid)
            {
                return Result<int>.Failure(0, "خطاء");
            }
            return await _mediator.Send(command);
        }
    }
}
