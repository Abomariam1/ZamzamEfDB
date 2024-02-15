using Zamzam.Application.Common.Mappings;
using Zamzam.Domain;

namespace Zamzam.Application.DTOs;

public class DepartmentDTO : IMapFrom<Department>
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public string? CreatedBy { get; set; }
    public ICollection<EmployeeDTO>? Employees { get; set; }

    public static explicit operator DepartmentDTO(Department v)
    {
        return new DepartmentDTO
        {
            DepartmentId = v.Id,
            DepartmentName = v.DepName,
            CreatedBy = v.CreatedBy,
            //Employees = (ICollection<EmployeeDTO>?)v.Employees
        };
    }
}
