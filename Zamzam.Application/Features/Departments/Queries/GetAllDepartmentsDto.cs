using Zamzam.Application.Common.Mappings;
using Zamzam.Domain;

namespace Zamzam.Application.Features.Departments.Queries
{
    public class GetAllDepartmentsDto : IMapFrom<Department>
    {
        public int DepartmentId { get; set; }
        public required string DepartmentName { get; set; }
        public string CreatedBy { get; set; } = "Anonymous";
    }
}
