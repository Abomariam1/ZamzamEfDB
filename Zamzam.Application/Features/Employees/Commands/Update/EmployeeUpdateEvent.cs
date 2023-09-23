using Zamzam.Domain;
using Zamzam.Domain.Common;

namespace Zamzam.Application.Features.Employees.Commands.Update
{
    public class EmployeeUpdateEvent : BaseEvent
    {
        public Employee Employee { get; }
        public EmployeeUpdateEvent(Employee employee)
        {
            Employee = employee;
        }
    }
}
