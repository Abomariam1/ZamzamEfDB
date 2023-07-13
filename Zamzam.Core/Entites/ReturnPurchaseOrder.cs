namespace Zamzam.Core
{
    public class ReturnPurchaseOrder : BaseOrder
    {
        public int PurchaaseOrderId { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual IList<ReturnPurchaseOrderLine> ReturnPurchaseOrderLines { get; set; }

    }
}
