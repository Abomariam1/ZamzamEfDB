using Zamzam.Domain;
using Zamzam.Domain.Common;

namespace Zamzam.Application.Features.ReturnPurchases.Commands.Create;
public class ReturnPurchasesCreatedEvent(ReturnPurchaseOrder returnPurchase): BaseEvent
{
    public ReturnPurchaseOrder ReturnPurchase { get; } = returnPurchase;

}
