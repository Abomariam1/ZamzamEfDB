using Zamzam.Domain;
using Zamzam.Domain.Common;

namespace Zamzam.Application.Features.InstallmentSaleOrders.Commands.Delete
{
    public class InstallmentSaleOrderDeletedEvent : BaseEvent
    {
        public InstallmentedSaleOrder InstallmentedSaleOrder { get; }

        public InstallmentSaleOrderDeletedEvent(InstallmentedSaleOrder installmentedSaleOrder)
        {
            InstallmentedSaleOrder = installmentedSaleOrder;
        }
    }
}
