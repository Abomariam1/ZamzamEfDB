using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamzam.Application.DTOs;
using Zamzam.Application.Features.Items.Commands.Create;
using Zamzam.Application.Features.Items.Commands.Delete;
using Zamzam.Application.Features.Items.Commands.Update;
using Zamzam.Application.Features.Items.Queries.GetAllItems;
using Zamzam.Shared;

namespace Zamzam.WebApi.Controllers.v1
{
    [Authorize]
    public class ItemController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public ItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<Result<List<ItemDTO>>> GetAllItems() =>
           await _mediator.Send(new GetAllItemsQuery());

        [HttpPost]
        public async Task<Result<ItemDTO>> Create(ItemCreateCommand command)
        {
            if (!ModelState.IsValid)
            {
                return Result<ItemDTO>.Failure("خطاء");
            }
            command.CreatedBy = HttpContext.User.Identity.Name;
            return await _mediator.Send(command);
        }
        [HttpPut]
        public async Task<Result<ItemDTO>> Update(ItemUpdateCommand command)
        {
            if (!ModelState.IsValid)
            {
                return Result<ItemDTO>.Failure("خطاء");
            }
            command.UpdatedBy = HttpContext.User.Identity.Name;
            return await _mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<Result<int>> Delete(int id)
        {
            ItemDeleteCommand? command = new ItemDeleteCommand(id);
            if (!ModelState.IsValid)
            {
                return Result<int>.Failure(0, "خطاء");
            }
            return await _mediator.Send(command);
        }
    }
}
