using Zamzam.Domain.Common;
using Zamzam.Domain.Entites;

namespace Zamzam.Application.Features.Sales.Commands.Update
{
    public class SaleOrderUpdatedEvent : BaseEvent
    {
        public SaleOrder SaleOrder { get; set; }
        public SaleOrderUpdatedEvent(SaleOrder saleOrder)
        {
            SaleOrder = saleOrder;
        }
    }
}
