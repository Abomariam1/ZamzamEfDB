using MediatR;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Domain.Entites;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Sales.Commands.Delete
{
    public record SaleOrderDeleteCommand(int orderId) : IRequest<Result<int>>;
    internal class SaleOrderDeleteCommandhandler : IRequestHandler<SaleOrderDeleteCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SaleOrderDeleteCommandhandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(SaleOrderDeleteCommand request, CancellationToken cancellationToken)
        {
            SaleOrder? order = await _unitOfWork.Repository<SaleOrder>().DeleteAsync(request.orderId);
            if (order == null)
            {
                return Result<int>.Failure(0, "لم يتم العثور على الفاتورة...");
            }
            if (order.OrderDetails.Count > 0)
            {
                await DeleteOrderDetails(order);
            }
            else
            {
                order.OrderDetails = _unitOfWork.Repository<OrderDetail>().Entities.Where(x => x.OrderId == order.Id).ToList();
                await DeleteOrderDetails(order);
            }
            order.AddDomainEvent(new SaleOrderDeletedEvent(order));

            await _unitOfWork.Save(cancellationToken);
            return await Result<int>.SuccessAsync(1, "تم حذف الفاتورة ....");
        }

        private async Task DeleteOrderDetails(SaleOrder order)
        {
            foreach (OrderDetail saleOrder in order.OrderDetails)
            {
                Item? item = await _unitOfWork.Repository<Item>().GetByIdAsync(saleOrder.ItemId);
                item.Balance += saleOrder.Quantity;
                saleOrder.IsDeleted = true;
            }
        }
    }
}

