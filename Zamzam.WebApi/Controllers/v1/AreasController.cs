using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamzam.Application.DTOs;
using Zamzam.Application.Features.Areas.Commands.CreateArea;
using Zamzam.Application.Features.Areas.Commands.DeleteArea;
using Zamzam.Application.Features.Areas.Commands.UpdateArea;
using Zamzam.Application.Features.Areas.Queries.GetAllAreas;
using Zamzam.Shared;

namespace Zamzam.WebApi.Controllers.v1;

[Authorize]
public class AreasController(IMediator mediator): ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<Result<List<AreaDto>>>> Get() =>
        await mediator.Send(new GetAllAreasQuery());

    [HttpPost]
    public async Task<ActionResult<Result<AreaDto>>> Create(CreateAreaCommand command)
    {
        if(!ModelState.IsValid)
        {
            return await Result<AreaDto>.FailureAsync("Not valid command");
        }
        command.CreatedBy = HttpContext.User.Identity?.Name;
        return await mediator.Send(command);
    }

    [HttpPut]
    public async Task<ActionResult<Result<AreaDto>>> Update(UpdateAreaCommand command)
    {
        if(!ModelState.IsValid)
        {
            return await Result<AreaDto>.FailureAsync("Not valid command");
        }
        command.UpdatedBy = HttpContext.User.Identity?.Name;
        return await mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Result<int>>> Delete(int id)
    {
        if(!ModelState.IsValid)
        {
            return await Result<int>.FailureAsync(0, "Not valid command");
        }
        return await mediator.Send(new AreaDeleteCommand(id));
    }
}
