using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zamzam.Application.Features.Areas.Queries;
using Zamzam.Shared;

namespace Zamzam.API.Controllers
{
    public class AreaController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public AreaController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<Result<List<GetAllAreaDto>>>> Get() =>
            await _mediator.Send(new GetAllAreasQuery());
    }
}
