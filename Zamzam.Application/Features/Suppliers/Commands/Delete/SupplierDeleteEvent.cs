using Zamzam.Domain;
using Zamzam.Domain.Common;

namespace Zamzam.Application.Features.Suppliers.Commands.Delete;

public class SupplierDeleteEvent : BaseEvent
{
    public Supplier Supplier { get; }

    public SupplierDeleteEvent(Supplier supplier)
    {
        Supplier = supplier;
    }
}
