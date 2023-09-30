using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamzam.API.Controllers;
using Zamzam.Application.DTOs;
using Zamzam.Application.Features.InstallmentSaleOrders.Commands.Create;
using Zamzam.Application.Features.Sales.Commands.Create;
using Zamzam.Application.Features.Sales.Commands.Update;
using Zamzam.Shared;

namespace Zamzam.WebApi.Controllers
{
    [Authorize]
    public class SaleOrderController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public SaleOrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("cash")]
        public async Task<Result<int>> CreateCash(SaleCreateCommand command)
        {
            if (!ModelState.IsValid)
            {
                return await Result<int>.FailureAsync(0, "Not valid command");
            }
            command.CreatedBy = HttpContext.User.Identity.Name;
            foreach (var o in command.OrderDetails)
            {
                o.CreatedBy = HttpContext.User.Identity.Name;
            }
            return await _mediator.Send(command);
        }

        [HttpPost("CashUpdate")]
        public async Task<Result<int>> CashUpdate(SaleOrederUpdateCommand command)
        {
            if (!ModelState.IsValid)
                return await Result<int>.FailureAsync(0, "يجب ادخال الحقول كاملة");
            command.UpdatedBy = HttpContext.User.Identity!.Name;
            foreach (ODetails o in command.OrderDetails!)
            {
                o.UpdateddBy = HttpContext.User.Identity.Name;
            }
            return await _mediator.Send(command);
        }

        [HttpPost("installment")]
        public async Task<Result<int>> Createinstallment(InstallmentSaleOrderCreateCommand command)
        {
            if (!ModelState.IsValid)
            {
                return await Result<int>.FailureAsync(0, "Not valid command");
            }
            command.CreatedBy = HttpContext.User.Identity.Name;
            foreach (var o in command.OrderDetails)
            {
                o.CreatedBy = HttpContext.User.Identity.Name;
            }
            return await _mediator.Send(command);
        }
    }
}
