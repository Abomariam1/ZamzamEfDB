using MediatR;
using Microsoft.EntityFrameworkCore;
using Zamzam.Application.DTOs;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Domain.Entites;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Sales.Commands.Update
{
    public record SaleOrderUpdateCommand : IRequest<Result<int>>
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now.Date;
        public decimal TotalPrice { get; set; }
        public decimal TotalDiscount { get; set; }
        public int EmployeeId { get; set; }
        public ICollection<ODetails>? OrderDetails { get; set; }
        public string? UpdatedBy { get; set; }
    }
    internal class SaleOrderUpdateCommandHandler : IRequestHandler<SaleOrderUpdateCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SaleOrderUpdateCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(SaleOrderUpdateCommand request, CancellationToken cancellationToken)
        {
            SaleOrder? DBOrder = await _unitOfWork.Repository<Order>().Entities
                .Include("OrderDetails")
                .FirstOrDefaultAsync(x => x.Id == request.OrderId, cancellationToken: cancellationToken) as SaleOrder;

            if (DBOrder == null)
                return Result<int>.Failure("لم يتم العثور على الفاتورة");

            List<OrderDetail> rqOrderDetails = new();

            foreach (var rq in request.OrderDetails)
            {
                OrderDetail detail = new()
                {
                    OrderId = (int)rq.OrderId!,
                    ItemId = rq.ItemId,
                    Price = rq.Price,
                    Quantity = rq.Quantity,
                    Discount = rq.Discount,
                    CreatedBy = rq.CreatedBy,
                };
                rqOrderDetails.Add(detail);
            }

            OrderDetalsCommand? orderDetail = new(rqOrderDetails, _unitOfWork);
            await orderDetail.UpdateSell();

            DBOrder.OrderDate = request.OrderDate;
            DBOrder.TotalPrice = request.TotalPrice;
            DBOrder.TotalDiscount = request.TotalDiscount;
            DBOrder.EmployeeId = request.EmployeeId;
            DBOrder.UpdatedBy = request.UpdatedBy;

            await _unitOfWork.Save(cancellationToken);
            return Result<int>.Success("تم تعديل الفاتورة...");
        }
    }
}
