using MediatR;
using Microsoft.EntityFrameworkCore;
using Zamzam.Application.Features.InstallmentSaleOrders.Commands.Create;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.InstallmentSaleOrders.Commands.Delete
{
    public record InstallmentSaleOrderDeleteCommand : IRequest<Result<int>>
    {
        public int Id { get; }

        public InstallmentSaleOrderDeleteCommand(int id)
        {
            Id = id;
        }

        public InstallmentSaleOrderDeleteCommand()
        {

        }
    }

    internal class InstallmentSaleOrderDeleteCommandHandler : IRequestHandler<InstallmentSaleOrderDeleteCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public InstallmentSaleOrderDeleteCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(InstallmentSaleOrderDeleteCommand request, CancellationToken cancellationToken)
        {
            InstallmentedSaleOrder? installOrder = await _unitOfWork.Repository<InstallmentedSaleOrder>()
                .Entities.Include("OrderDetails").FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
            if (installOrder == null)
                return await Result<int>.FailureAsync(0, "لم يتم العثور على الفاتورة");

            List<OrderDetail>? ordt = new();
            foreach (var order in installOrder.OrderDetails!)
            {
                OrderDetail detail = new()
                {
                    OrderId = (int)order.OrderId!,
                    ItemId = order.ItemId,
                    Quantity = order.Quantity,
                    Price = order.Price,
                    CreatedBy = order.CreatedBy,
                };
                ordt.Add(detail);
            }
            OrderDetalsCommand? orderDetails = new(ordt, _unitOfWork);
            installOrder.OrderDetails = await orderDetails.Delete();
            await _unitOfWork.Repository<InstallmentedSaleOrder>().DeleteAsync(installOrder.Id);
            installOrder.AddDomainEvent(new InstalmentOrderCreatedEvent(installOrder));
            await _unitOfWork.Save(cancellationToken);

            return await Result<int>.SuccessAsync(1, "تم حذف الفاتورة");
        }
    }
}
