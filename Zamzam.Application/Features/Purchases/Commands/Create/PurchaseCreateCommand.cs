using MediatR;
using Zamzam.Application.DTOs;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Domain.Types;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Purchases.Commands.Create
{
    public record PurchaseCreateCommand : IRequest<Result<int>>
    {
        public required DateTime OrderDate { get; set; }
        public required decimal TotalPrice { get; set; }
        public required decimal TotalDiscount { get; set; }
        public required InvoiceType InvoiceType { get; set; }
        public required int EmployeeId { get; set; }
        public required int SupplierId { get; set; }
        public required List<ODetails> Details { get; set; }
        public string? CreatedBy { get; set; }
    }

    internal record PurchaseCreateCommandHndler : IRequestHandler<PurchaseCreateCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseCreateCommandHndler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(PurchaseCreateCommand request, CancellationToken cancellationToken)
        {
            List<OrderDetail> details = new();
            foreach (ODetails requstDetails in request.Details)
            {
                var product = await _unitOfWork.Repository<Item>().GetByIdAsync(requstDetails.ItemId);
                if (product == null)
                    return await Result<int>.FailureAsync(0, $"لم يتم العثور على صنف");
                OrderDetail? detail = new()
                {
                    ItemId = requstDetails.ItemId,
                    Price = requstDetails.Price,
                    Quantity = requstDetails.Quantity,
                    Discount = requstDetails.Discount,
                    CreatedBy = requstDetails.CreatedBy
                };
                product.Balance += requstDetails.Quantity;

                details.Add(detail);
            }
            PurchaseOrder purchase = new()
            {
                SupplierId = request.SupplierId,
                OrderDate = request.OrderDate,
                TotalPrice = request.TotalPrice,
                TotalDiscount = request.TotalDiscount,
                OrderType = OrderType.Purchase,
                EmployeeId = request.EmployeeId,
                OrderDetails = details,
                CreatedBy = request.CreatedBy,
            };
            purchase.OrderDetails = details;
            PurchaseOrder? order = await _unitOfWork.Repository<PurchaseOrder>().AddAsync(purchase);
            order.AddDomainEvent(new PurchaseCreatedEvent(purchase));
            await _unitOfWork.Save(cancellationToken);
            return await Result<int>.SuccessAsync(0);
        }
    }
}
