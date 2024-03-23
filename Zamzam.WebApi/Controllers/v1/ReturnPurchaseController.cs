using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamzam.Application.Features.ReturnPurchases.Commands.Create;
using Zamzam.Shared;

namespace Zamzam.WebApi.Controllers.v1;

[Authorize]
public class ReturnPurchaseController(IMediator mediator): ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Result<int>>> AddReturnPurchasesInvoiceAsync(ReturnePurchaseCreateCommand command)
    {
        if(!ModelState.IsValid)
            return Result<int>.Failure("خطاء");
        return await mediator.Send(command);
    }
}
