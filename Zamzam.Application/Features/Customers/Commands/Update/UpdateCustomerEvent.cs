using Zamzam.Domain;
using Zamzam.Domain.Common;

namespace Zamzam.Application.Features.Customers.Commands.Update
{
    public class UpdateCustomerEvent : BaseEvent
    {
        public Customer Customer { get; set; }

        public UpdateCustomerEvent(Customer customer)
        {
            Customer = customer;
        }
    }
}
