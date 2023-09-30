using MediatR;
using Microsoft.EntityFrameworkCore;
using Zamzam.Application.DTOs;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Domain.Entites;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Sales.Commands.Update
{
    public record SaleOrederUpdateCommand : IRequest<Result<int>>
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now.Date;
        public decimal TotalPrice { get; set; }
        public decimal TotalDiscount { get; set; }
        public int EmployeeId { get; set; }
        public ICollection<ODetails>? OrderDetails { get; set; }
        public string? UpdatedBy { get; set; }
    }
    internal class SaleOrderUpdateCommandHandler : IRequestHandler<SaleOrederUpdateCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SaleOrderUpdateCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(SaleOrederUpdateCommand request, CancellationToken cancellationToken)
        {
            SaleOrder? DBOrder = await _unitOfWork.Repository<Order>().Entities
                .Include("OrderDetails")
                .FirstOrDefaultAsync(x => x.Id == request.OrderId, cancellationToken: cancellationToken) as SaleOrder;

            if (DBOrder == null)
                return Result<int>.Failure("لم يتم العثور على الفاتورة");

            DBOrder.OrderDate = request.OrderDate;
            DBOrder.TotalPrice = request.TotalPrice;
            DBOrder.TotalDiscount = request.TotalDiscount;
            DBOrder.EmployeeId = request.EmployeeId;
            DBOrder.UpdatedBy = request.UpdatedBy;
            OrderDetail? od = new();
            if (DBOrder.OrderDetails!.Count != request.OrderDetails!.Count)
            {
                if (request.OrderDetails.Count > DBOrder.OrderDetails.Count)
                {
                    foreach (ODetails Rq in request.OrderDetails)
                    {
                        od = DBOrder.OrderDetails.Where(x => x.ItemId == Rq.ItemId).FirstOrDefault();
                        if (od != null)
                        {
                            if (od!.Quantity != Rq!.Quantity)
                            {
                                await ChangItemBalanceAsync(Rq, od);
                            }
                            await _unitOfWork.Repository<OrderDetail>().UpdateAsync(FillOrderDetails(Rq, od));
                        }
                        else
                        {
                            OrderDetail newOD = new();
                            await _unitOfWork.Repository<OrderDetail>().AddAsync(FillOrderDetails(Rq, newOD));
                        }

                    }
                }
                else if (request.OrderDetails.Count < DBOrder.OrderDetails.Count)
                {

                    foreach (var OD in DBOrder.OrderDetails!)
                    {
                        if (request.OrderDetails.Count == 0)
                        {
                            await _unitOfWork.Repository<OrderDetail>().DeleteAsync(OD.Id);
                            continue;
                        }
                        ODetails? rd = request.OrderDetails!.Where(x => x.ItemId == OD.ItemId).FirstOrDefault();

                        if (OD.Quantity != rd!.Quantity)
                        {
                            await ChangItemBalanceAsync(rd, OD);
                        }
                        await _unitOfWork.Repository<OrderDetail>().UpdateAsync(FillOrderDetails(rd, od));
                        request.OrderDetails.Remove(rd);
                    }
                }
            }
            else
            {
                foreach (OrderDetail OD in DBOrder.OrderDetails!)
                {
                    ODetails? rd = request.OrderDetails!.Where(x => x.ItemId == OD.ItemId).FirstOrDefault();

                    if (OD.Quantity != rd!.Quantity)
                    {
                        await ChangItemBalanceAsync(rd, OD);
                    }
                    await _unitOfWork.Repository<OrderDetail>().UpdateAsync(FillOrderDetails(rd, OD));
                }
            }
            await _unitOfWork.Save(cancellationToken);
            return Result<int>.Success("تم تعديل الفاتورة...");
        }

        private async Task ChangItemBalanceAsync(ODetails Rq, OrderDetail? od)
        {
            Item? item = await _unitOfWork.Repository<Item>().GetByIdAsync(od!.ItemId);
            item.Balance += od.Quantity - Rq.Quantity;
            await _unitOfWork.Repository<Item>().UpdateAsync(item);
        }

        private OrderDetail FillOrderDetails(ODetails Rq, OrderDetail od)
        {
            od.TotalPrice = Rq.TotalPrice;
            od.ItemId = Rq.ItemId;
            od.Quantity = Rq.Quantity;
            od.Price = Rq.Price;
            od.Discount = Rq.Discount;
            od.UpdatedBy = Rq.UpdateddBy;
            od.UpdatedDate = DateTime.Now.ToLocalTime();
            return od;
        }
    }
}
