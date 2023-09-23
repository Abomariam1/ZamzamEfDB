using Zamzam.Domain;
using Zamzam.Domain.Common;

namespace Zamzam.Application.Features.Employees.Commands.Create
{
    public class EmployeeCreateEvent : BaseEvent
    {
        public EmployeeCreateEvent(Employee employee)
        {
            Employee = employee;
        }

        public Employee Employee { get; set; }
    }
}
