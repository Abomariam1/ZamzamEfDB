using Zamzam.Domain;
using Zamzam.Domain.Common;

namespace Zamzam.Application.Features.Employees.Commands.Create
{
    public class CustomerCreateEvent : BaseEvent
    {
        public CustomerCreateEvent(Customer customer)
        {
            Customer = customer;
        }

        public Customer Customer { get; set; }
    }
}
