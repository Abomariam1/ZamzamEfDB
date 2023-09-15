using AutoMapper;
using MediatR;
using Zamzam.Application.Common.Mappings;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Customers.Commands.Create
{
    public record CreateCustomerCommand : IRequest<Result<int>>, IMapFrom<Customer>
    {
        public string CustomerName { get; set; } = "عميل افتراضي";
        public string? Phone { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public long NationalCardId { get; set; } = 0;
        public string? Notes { get; set; } = string.Empty;
        public bool IsProplem { get; set; } = false;
        public bool IsBlackList { get; set; } = false;
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int AreaId { get; set; }
    }
    internal class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var cust = new Customer()
            {
                Name = request.CustomerName,
                Phone = request.Phone,
                Address = request.Address,
                NationalCardId = request.NationalCardId,
                Notes = request.Notes,
                IsProplem = request.IsProplem,
                IsBlackList = request.IsBlackList,
                AreaId = request.AreaId
            };
            await _unitOfWork.Repository<Customer>().AddAsync(cust);
            cust.AddDomainEvent(new CreatedCustomerEvent(cust));
            await _unitOfWork.Save(cancellationToken);
            return await Result<int>.SuccessAsync(cust.Id, "Customer created...");
        }
    }
}
