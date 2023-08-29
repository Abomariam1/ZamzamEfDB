using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zamzam.Application.Features.Areas.Commands.CreateArea;
using Zamzam.Application.Features.Areas.Commands.DeleteArea;
using Zamzam.Application.Features.Areas.Commands.UpdateArea;
using Zamzam.Application.Features.Areas.Queries;
using Zamzam.Shared;

namespace Zamzam.API.Controllers
{
    public class AreasController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public AreasController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<Result<List<GetAllAreasDto>>>> Get() =>
            await _mediator.Send(new GetAllAreasQuery());

        [HttpPost]
        public async Task<ActionResult<Result<int>>> Create(CreateAreaCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Result<int>>> Update(int id, UpdateAreaCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            return await _mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<int>>> Delete(int id)
        {
            return await _mediator.Send(new DeleteAreaCommand(id));
        }
    }
}
