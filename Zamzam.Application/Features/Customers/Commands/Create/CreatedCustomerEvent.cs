using Zamzam.Domain;
using Zamzam.Domain.Common;

namespace Zamzam.Application.Features.Customers.Commands.Create
{
    public class CreatedCustomerEvent : BaseEvent
    {
        public Customer Customer { get; }

        public CreatedCustomerEvent(Customer customer)
        {
            Customer = customer;
        }
    }
}
