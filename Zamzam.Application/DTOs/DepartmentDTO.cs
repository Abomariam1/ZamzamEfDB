using Zamzam.Application.Common.Mappings;
using Zamzam.Domain;

namespace Zamzam.Application.DTOs;

public class DepartmentDTO : IMapFrom<Department>
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; }
    public string? CreatedBy { get; set; }
    public ICollection<EmployeeDTO>? Employees { get; set; }
}
