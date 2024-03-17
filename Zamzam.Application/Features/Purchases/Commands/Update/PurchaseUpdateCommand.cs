using MediatR;
using Microsoft.EntityFrameworkCore;
using Zamzam.Application.DTOs;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Domain.Types;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Purchases.Commands.Update
{
    public record PurchaseUpdateCommand: IRequest<Result<int>>
    {
        public int OrderId { get; set; }
        public required DateTime OrderDate { get; set; }
        public required decimal TotalPrice { get; set; }
        public required decimal TotalDiscount { get; set; }
        public required InvoiceType InvoiceType { get; set; }
        public required int EmployeeId { get; set; }
        public required int SupplierId { get; set; }
        public List<ODetails> Details { get; set; }
        public string? UpdatedBy { get; set; }
    }

    internal class PurchaseUpdateCommandHandler(IUnitOfWork unitOfWork): IRequestHandler<PurchaseUpdateCommand, Result<int>>
    {

        public async Task<Result<int>> Handle(PurchaseUpdateCommand request, CancellationToken cancellationToken)
        {
            PurchaseOrder? oldOrder = await unitOfWork.Repository<PurchaseOrder>().Entities.Include("OrderDetail")
                .FirstOrDefaultAsync(x => x.Id == request.OrderId, cancellationToken: cancellationToken);
            if(oldOrder == null)
                return await Result<int>.FailureAsync(0, "لم يتم العثور على الفاتورة");
            List<Item> items = [];
            List<OrderDetail>? od = [];
            oldOrder.OrderDate = request.OrderDate;
            oldOrder.TotalPrice = request.TotalPrice;
            oldOrder.TotalDiscount = request.TotalDiscount;
            oldOrder.InvoiceType = request.InvoiceType;
            oldOrder.EmployeeId = request.EmployeeId;
            oldOrder.SupplierId = request.SupplierId;
            foreach(var dt in request.Details)
            {
                var newdt = new OrderDetail
                {
                    OrderId = dt.OrderId,
                    ItemId = dt.ItemId,
                    Price = dt.Price,
                    Quantity = dt.Quantity,
                    Discount = dt.Discount,
                };
                od.Add(newdt);
                Item? product = await unitOfWork.Repository<Item>().GetByIdAsync(dt.ItemId);
                var qt = oldOrder.OrderDetails.FirstOrDefault(x => x.OrderId == dt.OrderId && x.ItemId == dt.ItemId);
                product.Balance = qt.Quantity == dt.Quantity ? product.Balance : product.Balance;

            }
            oldOrder.OrderDetails = od;

            return await Result<int>.SuccessAsync(request.OrderId);
        }
    }
}
