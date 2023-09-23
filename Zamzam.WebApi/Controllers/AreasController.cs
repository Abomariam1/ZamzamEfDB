using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamzam.Application.Features.Areas.Commands.CreateArea;
using Zamzam.Application.Features.Areas.Commands.DeleteArea;
using Zamzam.Application.Features.Areas.Commands.UpdateArea;
using Zamzam.Application.Features.Areas.Queries.GetAllAreas;
using Zamzam.Shared;

namespace Zamzam.API.Controllers
{
    [Authorize]
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
            command.CreatedBy = HttpContext.User.Identity.Name;
            return await _mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Result<int>>> Update(int id, UpdateAreaCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            command.UpdatedBy = HttpContext.User.Identity.Name;
            return await _mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<int>>> Delete(int id) =>
            await _mediator.Send(new DeleteAreaCommand(id));
    }
}
