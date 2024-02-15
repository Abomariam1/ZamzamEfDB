using AutoMapper;
using MediatR;
using Zamzam.Application.Common.Mappings;
using Zamzam.Application.DTOs;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Employees.Commands.Create
{
    public record EmployeeCreateCommand : IRequest<Result<EmployeeDTO>>, IMapFrom<Employee>
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string? PostalCode { get; set; } = string.Empty;
        public long NationalId { get; set; }
        public string Titel { get; set; } = string.Empty;
        public DateTime HireDate { get; set; }
        public DateTime BirthDate { get; set; }
        public decimal Salary { get; set; }
        public string? Qualification { get; set; } = string.Empty;
        public string? Photo { get; set; }
        public int DepartmentId { get; set; }
        public string? CreatedBy { get; set; }
    }
    internal class EmployeeCreateCommandHandler : IRequestHandler<EmployeeCreateCommand, Result<EmployeeDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeCreateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<EmployeeDTO>> Handle(EmployeeCreateCommand request, CancellationToken cancellationToken)
        {
            Employee Emp = new()
            {
                Name = request.EmployeeName,
                Phone = request.Phone,
                Address = request.Address,
                City = request.City,
                PostalCode = request.PostalCode,
                Country = request.Country,
                Region = request.Region,
                NationalId = request.NationalId,
                Titel = request.Titel,
                HireDate = request.HireDate,
                BirthDate = request.BirthDate,
                Salary = request.Salary,
                Qualification = request.Qualification!,
                Photo = Convert.FromBase64String(request.Photo),
                DepartmentId = request.DepartmentId,
                CreatedBy = request.CreatedBy,
                CreatedDate = DateTime.Now
            };
            var newEmployee = await _unitOfWork.Repository<Employee>().AddAsync(Emp);
            newEmployee.AddDomainEvent(new EmployeeCreateEvent(newEmployee));
            int count = await _unitOfWork.Save(cancellationToken);
            EmployeeDTO? emp = _mapper.Map<EmployeeDTO>(newEmployee);
            return count > 0 ? Result<EmployeeDTO>.Success(emp, $"تم انشاء الموظف  {newEmployee.Name} بنجاح.") :
                Result<EmployeeDTO>.Failure($"فشل في انشاء الموظف {emp.EmployeeName}.");
        }
    }
}

