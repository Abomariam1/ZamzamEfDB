using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.InstallmentPayment.Commands.Create
{
    public record InstallmentPaymentCreateCommand : IRequest<Result<int>>
    {
        public int OrderId { get; set; }
        public DateTime PayedOn { get; set; }
        public decimal Value { get; set; }
        [MaxLength(100)]
        public string Notes { get; set; }
        public int EmployeeId { get; set; }
    }
    internal class InstallmentPaymentCreateCommandHandler : IRequestHandler<InstallmentPaymentCreateCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public InstallmentPaymentCreateCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(InstallmentPaymentCreateCommand request, CancellationToken cancellationToken)
        {
            InstallmentedSaleOrder? payment = await _unitOfWork.Repository<InstallmentedSaleOrder>().Entities
                .Include("Customer").Include("OrderDetail")
                .FirstOrDefaultAsync(x => x.Id == request.OrderId, cancellationToken: cancellationToken);
            if (payment == null)
                return Result<int>.Failure(0, "لم يتم العثور على الفاتورة");
            Installment? order = new()
            {
                OrderId = request.OrderId,
                PayedOn = request.PayedOn,
                Value = request.Value,
                Notes = request.Notes,
                EmployeeId = request.EmployeeId,
            };
            Installment? orderAdded = await _unitOfWork.Repository<Installment>().AddAsync(order);
            orderAdded.AddDomainEvent(new InstallmentPaymentCreatedEvent(order));
            await _unitOfWork.Save(cancellationToken);
            return await Result<int>.SuccessAsync(1, $"تم اضافة قسط للعميل {payment.Customer.Name}");
        }
    }
}
