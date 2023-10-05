using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Zamzam.Application.Features.InstallmentPayment.Commands.Create;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.InstallmentPayment.Commands.Update
{
    public record InstallmentPaymentUpdateCommand : IRequest<Result<int>>
    {
        public int InstallmentId { get; set; }
        public int OrderId { get; set; }
        public DateTime PayedOn { get; set; }
        public decimal Value { get; set; }
        [MaxLength(100)]
        public string Notes { get; set; }
        public int EmployeeId { get; set; }
        public string UpdatedBy { get; set; }
    }

    internal class InstallmetPaymentUpdateCommandHandler : IRequestHandler<InstallmentPaymentUpdateCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public InstallmetPaymentUpdateCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(InstallmentPaymentUpdateCommand request, CancellationToken cancellationToken)
        {
            Installment? payment = await _unitOfWork.Repository<Installment>().Entities.Include("Customer")
                .FirstOrDefaultAsync(x => x.Id == request.InstallmentId, cancellationToken);
            if (payment == null)
                return Result<int>.Failure(0, "لم يتم العثور على القسط");

            payment.PayedOn = request.PayedOn;
            payment.Value = request.Value;
            payment.Notes = request.Notes;
            payment.EmployeeId = request.EmployeeId;
            payment.UpdatedBy = request.UpdatedBy;

            Installment? orderAdded = await _unitOfWork.Repository<Installment>().AddAsync(payment);
            orderAdded.AddDomainEvent(new InstallmentPaymentCreatedEvent(payment));
            await _unitOfWork.Save(cancellationToken);
            return await Result<int>.SuccessAsync(1, $"تم اضافة قسط للعميل {payment.Customer.Name}");
        }
    }
}
