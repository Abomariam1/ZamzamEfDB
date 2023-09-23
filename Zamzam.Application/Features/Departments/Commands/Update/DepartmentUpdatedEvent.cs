using Zamzam.Domain;
using Zamzam.Domain.Common;

namespace Zamzam.Application.Features.Departments.Commands.Update
{
    public class DepartmentUpdatedEvent : BaseEvent
    {
        public Department Department { get; set; }

        public DepartmentUpdatedEvent(Department department)
        {
            Department = department;
        }
    }
}
