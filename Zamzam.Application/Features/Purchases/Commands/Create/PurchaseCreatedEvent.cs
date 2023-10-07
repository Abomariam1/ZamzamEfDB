using Zamzam.Domain;
using Zamzam.Domain.Common;

namespace Zamzam.Application.Features.Purchases.Commands.Create
{
    public class PurchaseCreatedEvent : BaseEvent
    {
        public PurchaseOrder PurchaseOrder { get; }

        public PurchaseCreatedEvent(PurchaseOrder purchaseOrder)
        {
            PurchaseOrder = purchaseOrder;
        }
    }
}
