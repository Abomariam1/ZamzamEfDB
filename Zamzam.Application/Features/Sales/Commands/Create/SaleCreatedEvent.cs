using Zamzam.Domain.Common;
using Zamzam.Domain.Entites;

namespace Zamzam.Application.Features.Sales.Commands.Create
{
    public class SaleCreatedEvent : BaseEvent
    {
        public SaleOrder SaleOrder { get; }
        public SaleCreatedEvent(SaleOrder saleOrder)
        {
            SaleOrder = saleOrder;
        }
    }
}
