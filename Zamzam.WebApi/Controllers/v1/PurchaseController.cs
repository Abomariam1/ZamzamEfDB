using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zamzam.Application.DTOs;
using Zamzam.Application.Features.Purchases.Commands.Create;
using Zamzam.Shared;

namespace Zamzam.WebApi.Controllers.v1;

public class PurchaseController: ApiControllerBase
{
    private readonly IMediator _mediator;

    public PurchaseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Result<PurchaseDto>>> AddPurchase(PurchaseCreateCommand command)
    {
        if(!ModelState.IsValid)
        {
            return await Result<PurchaseDto>.FailureAsync("يجب ادخال كافة البيانات المطلوبة");
        }
        return await _mediator.Send(command);
    }
}
