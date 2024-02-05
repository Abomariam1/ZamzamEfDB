using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Zamzam.Application.Common.Mappings;
using Zamzam.Application.DTOs;
using Zamzam.Application.Interfaces.Repositories;
using Zamzam.Domain;
using Zamzam.Shared;

namespace Zamzam.Application.Features.Employees.Commands.Update
{
    public record EmployeeUpdateCommand : IRequest<Result<EmployeeDTO>>, IMapFrom<Employee>
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
        public DepartmentDTO? Department { get; set; }
        public string? UpdatedBy { get; set; }
    }

    internal class EmployeeUpdateCommandHandler : IRequestHandler<EmployeeUpdateCommand, Result<EmployeeDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeUpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<EmployeeDTO>> Handle(EmployeeUpdateCommand request, CancellationToken cancellationToken)
        {
            Employee employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(request.EmployeeId);
            if (employee == null)
                return Result<EmployeeDTO>.Failure(null, "The Employee not found");
            employee.Name = request.EmployeeName;
            employee.Phone = request.Phone;
            employee.Address = request.Address;
            employee.NationalId = request.NationalId;
            employee.City = request.City;
            employee.Region = request.Region;
            employee.Country = request.Country;
            employee.PostalCode = request.PostalCode;
            employee.Titel = request.Titel;
            employee.HireDate = request.HireDate;
            employee.BirthDate = request.BirthDate;
            employee.Salary = request.Salary;
            employee.Qualification = request.Qualification;
            employee.Photo = Encoding.UTF8.GetBytes(request.Photo ?? "");
            employee.DepartmentId = request.DepartmentId;
            employee.UpdatedBy = request.UpdatedBy;
            Employee? result = await _unitOfWork.Repository<Employee>().UpdateAsync(employee);
            result.AddDomainEvent(new EmployeeUpdateEvent(result));
            await _unitOfWork.Save(cancellationToken);
            EmployeeDTO? updated = _unitOfWork.Repository<Employee>().Entities
                .Where(x => x.IsDeleted == false && x.Id == result.Id)
                .Include(x => x.Department)
                .ProjectTo<EmployeeDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefault();
            return Result<EmployeeDTO>.Success(updated!, $"تم تعديل العميل {updated!.EmployeeName}...");
        }
    }
}
