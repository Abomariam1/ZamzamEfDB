using AutoMapper;
using MediatR;
using Zamzam.Application.Common.Mappings;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Customers.Commands.Update
{
    public record UpdateCustomerCommand : IRequest<Result<int>>, IMapFrom<Customer>
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = "عميل افتراضي";
        public string? Phone { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public long NationalCardId { get; set; } = 0;
        public string? Notes { get; set; } = string.Empty;
        public bool IsProplem { get; set; } = false;
        public bool IsBlackList { get; set; } = false;
        public int AreaId { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;
    }
    internal class UpdateCustomerCommanHandler : IRequestHandler<UpdateCustomerCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCustomerCommanHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            Customer cust = await _unitOfWork.Repository<Customer>().GetByIdAsync(request.Id);
            if (cust == null)
            {
                return Result<int>.Failure("Customer not found");
            }
            else
            {
                cust.Name = request.CustomerName;
                cust.Phone = request.Phone;
                cust.Address = request.Address;
                cust.NationalCardId = request.NationalCardId;
                cust.Notes = request.Notes;
                cust.IsProplem = request.IsProplem;
                cust.IsBlackList = request.IsBlackList;
                cust.AreaId = request.AreaId;
                cust.UpdatedBy = request.UpdatedBy;

                await _unitOfWork.Repository<Customer>().UpdateAsync(cust);
                cust.AddDomainEvent(new UpdateCustomerEvent(cust));
                await _unitOfWork.Save(cancellationToken);
                return Result<int>.Success(cust.Id, "Customer Updated...");
            }


        }
    }
}
