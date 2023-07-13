namespace Zamzam.Core
{
    public class PurchaseOrderLine : BaseOrderLine
    {
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public virtual ReturnPurchaseOrderLine ReturnPurchaseOrder { get; set; }
        public virtual Item Item { get; set; }
    }
}
