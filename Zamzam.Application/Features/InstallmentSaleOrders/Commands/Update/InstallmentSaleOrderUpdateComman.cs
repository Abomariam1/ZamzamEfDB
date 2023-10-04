using MediatR;
using Zamzam.Application.DTOs;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.InstallmentSaleOrders.Commands.Update
{
    public record InstallmentSaleOrderUpdateComman : IRequest<Result<int>>
    {
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal Payed { get; set; }
        public decimal Remain { get; set; }
        public decimal InstallmentValue { get; set; }
        public int InstallmentPeriodInMonths { get; set; }
        public int EmployeeId { get; set; }
        public ICollection<ODetails>? OrderDetails { get; set; }
        public string? UpdatedBy { get; set; }
    }
    public class InstallmentSaleOrderUpdateCommanHandler : IRequestHandler<InstallmentSaleOrderUpdateComman, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public InstallmentSaleOrderUpdateCommanHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(InstallmentSaleOrderUpdateComman request, CancellationToken cancellationToken)
        {

            if (request.OrderId == 0)
                return await Result<int>.FailureAsync(0, "يجب ادخال البيانات كاملة");
            InstallmentedSaleOrder? dbOrder = await _unitOfWork.Repository<InstallmentedSaleOrder>().GetByIdAsync(request.OrderId);
            if (dbOrder == null)
                return await Result<int>.FailureAsync(0, "لم يتم ايجاد الفاتورة");


            return await Result<int>.SuccessAsync(request.CustomerId);
        }
    }
}
