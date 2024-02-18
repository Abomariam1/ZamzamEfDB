using MediatR;
using Zamzam.Application.DTOs;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Customers.Commands.Create
{
    public record CreateCustomerCommand : IRequest<Result<CustomerDto>>
    {
        public string CustomerName { get; set; } = "عميل افتراضي";
        public string? Phone { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public long NationalCardId { get; set; } = 0;
        public string? Notes { get; set; } = string.Empty;
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int AreaId { get; set; }
    }
    internal class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Result<CustomerDto>>
    {
        private readonly IUnitOfWork _unitOfWork;


        public CreateCustomerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<Result<CustomerDto>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var cust = new Customer()
            {
                Name = request.CustomerName,
                Phone = request.Phone,
                Address = request.Address,
                NationalCardId = request.NationalCardId,
                Notes = request.Notes,
                AreaId = request.AreaId,
                CreatedBy = request.CreatedBy
            };
            Customer? added = await _unitOfWork.Repository<Customer>().AddAsync(cust);
            int count = await _unitOfWork.Save(cancellationToken);
            if (count > 0)
            {
                cust.AddDomainEvent(new CreatedCustomerEvent(cust));
                return await Result<CustomerDto>.SuccessAsync((CustomerDto)added, $"تم اضاقة العميل {added.Name}...");
            }
            return await Result<CustomerDto>.FailureAsync("فشل في اضافة العميل");
        }
    }
}
