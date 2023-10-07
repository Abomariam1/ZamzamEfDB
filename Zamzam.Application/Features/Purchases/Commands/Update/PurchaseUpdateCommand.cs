using MediatR;
using Microsoft.EntityFrameworkCore;
using Zamzam.Application.DTOs;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Domain.Types;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Purchases.Commands.Update
{
    public record PurchaseUpdateCommand : IRequest<Result<int>>
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

    internal class PurchaseUpdateCommandHandler : IRequestHandler<PurchaseUpdateCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseUpdateCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(PurchaseUpdateCommand request, CancellationToken cancellationToken)
        {
            PurchaseOrder? order = await _unitOfWork.Repository<PurchaseOrder>().Entities.Include("OrderDetail")
                .FirstOrDefaultAsync(x => x.Id == request.OrderId);
            if (order == null)
                return await Result<int>.FailureAsync(0, "لم يتم العثور على الفاتورة");
            order.OrderDate = request.OrderDate;
            order.TotalPrice = request.TotalPrice;
            order.TotalDiscount = request.TotalDiscount;
            order.InvoiceType = request.InvoiceType;
            order.EmployeeId = request.EmployeeId;
            order.SupplierId = request.SupplierId;

            return await Result<int>.SuccessAsync(request.OrderId);
        }
    }
}
