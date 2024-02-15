using Zamzam.Application.Common.Mappings;
using Zamzam.Domain;

namespace Zamzam.Application.DTOs;

public class EmployeeDTO : IMapFrom<Employee>
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
    public int DeoartmentId { get; set; }
    public DepartmentDTO? Department { get; set; }
    public string? CreatedBy { get; set; }

    public static explicit operator EmployeeDTO(Employee v)
    {
        if (v == null) return new();
        return new()
        {
            EmployeeId = v.Id,
            EmployeeName = v.Name,
            Phone = v.Phone,
            Address = v.Address,
            City = v.City,
            Region = v.Region,
            Country = v.Country,
            PostalCode = v.PostalCode,
            BirthDate = v.BirthDate,
            HireDate = v.HireDate,
            NationalId = v.NationalId,
            Titel = v.Titel,
            Qualification = v.Qualification,
            Salary = v.Salary,
            Photo = Convert.ToBase64String(v.Photo ?? []),
            DeoartmentId = v.DepartmentId,
            CreatedBy = v.CreatedBy,
            Department = (DepartmentDTO)v.Department
        };
    }
}