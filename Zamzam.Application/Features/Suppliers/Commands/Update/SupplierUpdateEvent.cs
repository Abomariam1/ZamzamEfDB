using Zamzam.Domain;
using Zamzam.Domain.Common;

namespace Zamzam.Application.Features.Suppliers.Commands.Update;

public class SupplierUpdateEvent : BaseEvent
{
    public Supplier Supplier { get; }

    public SupplierUpdateEvent(Supplier supplier)
    {
        Supplier = supplier;
    }
}
