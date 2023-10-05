using Zamzam.Domain;
using Zamzam.Domain.Common;

namespace Zamzam.Application.Features.InstallmentPayment.Commands.Create
{
    public class InstallmentPaymentCreatedEvent : BaseEvent
    {
        public Installment Installment { get; }
        public InstallmentPaymentCreatedEvent(Installment installment)
        {
            Installment = installment;
        }
    }
}
