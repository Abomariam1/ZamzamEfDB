using Zamzam.Domain.Common;

namespace Zamzam.Domain
{
    public class ReturnPurchaseOrder : BaseAuditableEntity
    {
        public string ReasonForReturn { get; set; }
    }
}
