using Zamzam.Domain;
using Zamzam.Domain.Common;

namespace Zamzam.Application.Features.InstallmentPayment.Commands.Update
{
    public class InstallmentPaymentUpdatedEvent : BaseEvent
    {
        public Installment Installment { get; }

        public InstallmentPaymentUpdatedEvent(Installment installment)
        {
            Installment = installment;
        }
    }
}
