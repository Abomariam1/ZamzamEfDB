using Zamzam.Domain;
using Zamzam.Domain.Common;

namespace Zamzam.Application.Features.Customers.Commands.Delete
{
    public class DeleteCustomerEvent : BaseEvent
    {
        public Customer Customer { get; set; }

        public DeleteCustomerEvent(Customer customer)
        {
            Customer = customer;
        }
    }
}
