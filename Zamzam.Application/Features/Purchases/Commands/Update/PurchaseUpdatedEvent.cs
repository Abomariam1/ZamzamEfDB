using Zamzam.Domain;
using Zamzam.Domain.Common;

namespace Zamzam.Application.Features.Purchases.Commands.Update
{
    public class PurchaseUpdatedEvent : BaseEvent
    {
        public PurchaseOrder PurchaseOrder { get; }

        public PurchaseUpdatedEvent(PurchaseOrder purchaseOrder)
        {
            PurchaseOrder = purchaseOrder;
        }
    }
}
