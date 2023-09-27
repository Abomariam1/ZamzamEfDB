using Zamzam.Domain;
using Zamzam.Domain.Common;

namespace Zamzam.Application.Features.InstallmentSaleOrders.Commands.Create
{
    public class InstalmentOrderCreatedEvent : BaseEvent
    {
        public InstallmentedSaleOrder installmented { get; }
        public InstalmentOrderCreatedEvent(InstallmentedSaleOrder installmented)
        {
            this.installmented = installmented;
        }
    }
}
