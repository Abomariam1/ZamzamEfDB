using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zamzam.Application.DTOs;
using Zamzam.Application.Features.Purchases.Commands.Create;
using Zamzam.Application.Features.Purchases.Queries;
using Zamzam.Shared;

namespace Zamzam.WebApi.Controllers.v1;

public class PurchaseController(IMediator mediator): ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Result<PurchaseDto>>> AddPurchase(PurchaseCreateCommand command)
    {
        if(!ModelState.IsValid)
        {
            return await Result<PurchaseDto>.FailureAsync("يجب ادخال كافة البيانات المطلوبة");
        }
        command.CreatedBy = HttpContext.User.Identity?.Name;
        return await mediator.Send(command);
    }
    [HttpGet("GetSuppInvIdList")]
    public async Task<ActionResult<Result<List<PurchacesSuppInvDto>>>> GetInvoicesList() =>
        await mediator.Send(new GetInvoicesListQuery());

    [HttpGet("{id}")]
    public async Task<ActionResult<Result<PurchaseDto>>> GetById(int id) =>
        await mediator.Send(new GetPurchaseByIdQuery(id));



}
