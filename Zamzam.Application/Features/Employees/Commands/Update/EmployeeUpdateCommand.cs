using AutoMapper;
using MediatR;
using Zamzam.Application.Common.Mappings;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Employees.Commands.Update
{
    public record EmployeeUpdateCommand : IRequest<Result<int>>, IMapFrom<Employee>
    {
        public int Id { get; set; }
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
        public string Qualification { get; set; } = string.Empty;
        public byte[]? Photo { get; set; }
        public int DepartmentId { get; set; }
        public string UpdatedBy { get; set; }
    }

    internal class EmployeeUpdateCommandHandler : IRequestHandler<EmployeeUpdateCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeUpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(EmployeeUpdateCommand request, CancellationToken cancellationToken)
        {
            Employee employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(request.Id);
            if (employee == null)
                return Result<int>.Failure(0, "The Employee not found");
            employee.Name = request.Name;
            employee.Phone = request.Phone;
            employee.Address = request.Address;
            employee.NationalId = request.NCardlId;
            employee.City = request.City;
            employee.Region = request.Region;
            employee.Country = request.Country;
            employee.PostalCode = request.PostalCode;
            employee.Titel = request.Titel;
            employee.HireDate = request.HireDate;
            employee.BirthDate = request.BirthDate;
            employee.Salary = request.Salary;
            employee.Qualification = request.Qualification;
            employee.Photo = request.Photo;
            employee.DepartmentId = request.DepartmentId;
            employee.UpdatedBy = request.UpdatedBy;
            employee.UpdatedDate = DateTime.Now;
            var result = await _unitOfWork.Repository<Employee>().UpdateAsync(employee);
            result.AddDomainEvent(new EmployeeUpdateEvent(result));
            await _unitOfWork.Save(cancellationToken);
            return Result<int>.Success(request.Id, $"تم تعديل العميل {request.Name}...");
        }
    }
}
