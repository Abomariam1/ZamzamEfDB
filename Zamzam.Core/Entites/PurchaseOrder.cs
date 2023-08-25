using Zamzam.Domain.Common;

namespace Zamzam.Domain
{
    public class PurchaseOrder : BaseAuditableEntity
    {
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
