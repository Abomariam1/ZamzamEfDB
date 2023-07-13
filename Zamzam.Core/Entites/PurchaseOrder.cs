namespace Zamzam.Core
{
    public class PurchaseOrder : BaseOrder
    {
        public int SupplierId { get; set; }
        public virtual ReturnPurchaseOrder ReturnPurchaseOrder { get; set; }
        public Supplier Supplier { get; set; }
        public virtual ICollection<PurchaseOrderLine> PurchaseOrderLines { get; set; }
    }
}
