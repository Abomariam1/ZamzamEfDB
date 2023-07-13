namespace Zamzam.Core
{
    public class ReturnPurchaseOrderLine : BaseOrderLine
    {
        public int RerturnPurchaseOrderId { get; set; }
        public virtual ReturnPurchaseOrder ReturnPurcheseOrder { get; set; }
        public virtual PurchaseOrderLine PurchaseOrderLine { get; set; }
    }
}
