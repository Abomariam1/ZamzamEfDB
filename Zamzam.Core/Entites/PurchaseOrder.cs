namespace Zamzam.Core
{
    public class PurchaseOrder : BaseOrder
    {
        public Supplier Supplier { get; set; }
        public virtual ICollection<PurchaseOrderLine> PurchaseOrderLines { get; set; }
    }
}
