using Zamzam.Domain;
using Zamzam.Domain.Common;

namespace Zamzam.Application.Features.Employees.Commands.Delete
{
    public class EmployeeDeleteEvent : BaseEvent
    {
        public Employee Employee { get; set; }
        public EmployeeDeleteEvent(Employee employee)
        {
            Employee = employee;
        }
    }
}
