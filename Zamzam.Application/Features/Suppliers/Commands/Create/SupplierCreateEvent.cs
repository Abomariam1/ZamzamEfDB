using Zamzam.Domain;
using Zamzam.Domain.Common;

namespace Zamzam.Application.Features.Suppliers.Commands.Create
{
    public class SupplierCreateEvent : BaseEvent
    {
        public Supplier Supplier { get; }

        public SupplierCreateEvent(Supplier supplier)
        {
            Supplier = supplier;
        }
    }
}
