namespace Zamzam.Core
{
    public class PurchaseOrder : BaseOrder
    {
        public Guid EmployeeId { get; set; }
        public Guid SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<ReturnPurchaseOrder> ReturnPurchaseOrders { get; set; }
        public virtual ICollection<PurchaseOrderLine> PurchaseOrderLines { get; set; }
    }
}
