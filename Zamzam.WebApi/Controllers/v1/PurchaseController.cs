using MediatR;

namespace Zamzam.WebApi.Controllers.v1;

public class PurchaseController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public PurchaseController(IMediator mediator)
    {
        _mediator = mediator;
    }
}
