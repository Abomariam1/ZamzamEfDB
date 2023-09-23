using AutoMapper;
using MediatR;
using Zamzam.Application.Common.Mappings;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Employees.Commands.Create
{
    public record CustomerCreateCommand : IRequest<Result<int>>, IMapFrom<Customer>
    {
        public string Name { get; set; } = "عميل افتراضي";
        public string? Phone { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public long NCId { get; set; } = 0;
        public string? Notes { get; set; } = string.Empty;
        public int AreaId { get; set; }
    }
    internal class CustomerCreateCommandHandler : IRequestHandler<CustomerCreateCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerCreateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CustomerCreateCommand request, CancellationToken cancellationToken)
        {
            Customer cust = new()
            {
                Name = request.Name,
                Phone = request.Phone,
                Address = request.Address,
                NationalCardId = request.NCId,
                Notes = request.Notes,
                AreaId = request.AreaId
            };
            var created = await _unitOfWork.Repository<Customer>().AddAsync(cust);
            created.AddDomainEvent(new CustomerCreateEvent(created));
            var count = await _unitOfWork.Save(cancellationToken);
            return Result<int>.Success(count, $"Customer {created.Name} is created...");
        }
    }
}

