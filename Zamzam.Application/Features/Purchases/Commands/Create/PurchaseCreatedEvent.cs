using Zamzam.Domain;
using Zamzam.Domain.Common;

namespace Zamzam.Application.Features.Purchases.Commands.Create
{
    public class PurchaseCreatedEvent(PurchaseOrder purchaseOrder): BaseEvent
    {
        public PurchaseOrder PurchaseOrder { get; } = purchaseOrder;
    }
}
