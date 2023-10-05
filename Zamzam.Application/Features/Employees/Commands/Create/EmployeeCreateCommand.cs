using AutoMapper;
using MediatR;
using Zamzam.Application.Common.Mappings;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Employees.Commands.Create
{
    public record EmployeeCreateCommand : IRequest<Result<int>>, IMapFrom<Employee>
    {
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string? PostalCode { get; set; } = string.Empty;
        public long NCardlId { get; set; }
        public string Titel { get; set; } = string.Empty;
        public DateTime HireDate { get; set; }
        public DateTime BirthDate { get; set; }
        public decimal Salary { get; set; }
        public string? Qualification { get; set; } = string.Empty;
        public string? Photo { get; set; }
        public int DepartmentId { get; set; }
        public string? CreatedBy { get; set; }
    }
    internal class EmployeeCreateCommandHandler : IRequestHandler<EmployeeCreateCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeCreateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(EmployeeCreateCommand request, CancellationToken cancellationToken)
        {
            Employee cust = new()
            {
                Name = request.Name,
                Phone = request.Phone,
                Address = request.Address,
                City = request.City,
                PostalCode = request.PostalCode,
                Country = request.Country,
                Region = request.Region,
                NationalId = request.NCardlId,
                Titel = request.Titel,
                HireDate = request.HireDate,
                BirthDate = request.BirthDate,
                Salary = request.Salary,
                Qualification = request.Qualification!,
                Photo = Convert.FromBase64String(request.Photo!),
                DepartmentId = request.DepartmentId,
                CreatedBy = request.CreatedBy,
                CreatedDate = DateTime.Now
            };
            var created = await _unitOfWork.Repository<Employee>().AddAsync(cust);
            created.AddDomainEvent(new EmployeeCreateEvent(created));
            var count = await _unitOfWork.Save(cancellationToken);
            return Result<int>.Success(count, $"Employee {created.Name} is created...");
        }
    }
}

