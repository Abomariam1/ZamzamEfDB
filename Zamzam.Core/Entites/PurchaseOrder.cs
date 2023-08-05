namespace Zamzam.Core
{
    public class PurchaseOrder : BaseOrder
    {
        public Ulid EmployeeId { get; set; }
        public Ulid SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<ReturnPurchaseOrder> ReturnPurchaseOrders { get; set; }
        public virtual ICollection<PurchaseOrderLine> PurchaseOrderLines { get; set; }
    }
}
