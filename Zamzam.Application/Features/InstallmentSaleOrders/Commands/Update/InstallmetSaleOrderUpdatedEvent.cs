using Zamzam.Domain;
using Zamzam.Domain.Common;

namespace Zamzam.Application.Features.InstallmentSaleOrders.Commands.Update
{
    public class InstallmetSaleOrderUpdatedEvent : BaseEvent
    {
        public InstallmentedSaleOrder Installment { get; set; }
        public InstallmetSaleOrderUpdatedEvent(InstallmentedSaleOrder installment)
        {
            Installment = installment;
        }
    }
}
