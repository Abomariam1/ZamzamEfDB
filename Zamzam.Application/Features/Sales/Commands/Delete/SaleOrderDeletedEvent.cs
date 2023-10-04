using Zamzam.Domain.Common;
using Zamzam.Domain.Entites;

namespace Zamzam.Application.Features.Sales.Commands.Delete
{
    public class SaleOrderDeletedEvent : BaseEvent
    {
        public SaleOrder SaleOrder { get; set; }
        public SaleOrderDeletedEvent(SaleOrder saleOrder)
        {
            SaleOrder = saleOrder;
        }
    }
}
