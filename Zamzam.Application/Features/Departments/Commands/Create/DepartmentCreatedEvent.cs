using Zamzam.Domain;
using Zamzam.Domain.Common;

namespace Zamzam.Application.Features.Departments.Commands.Create
{
    public class DepartmentCreatedEvent : BaseEvent
    {
        private Department dep;

        public DepartmentCreatedEvent(Department dep)
        {
            this.dep = dep;
        }
    }
}
