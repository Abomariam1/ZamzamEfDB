using Zamzam.Domain.Common;

namespace Zamzam.Domain
{
    public class Supplier : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        //public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }

    }
}
