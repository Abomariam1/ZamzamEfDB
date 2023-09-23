using Zamzam.Domain;
using Zamzam.Domain.Common;

namespace Zamzam.Application.Features.Departments.Commands.Delete
{
    public class DepartmentDeletedEvent : BaseEvent
    {
        public Department Department { get; }
        public DepartmentDeletedEvent(Department department)
        {
            Department = department;
        }

    }
}
