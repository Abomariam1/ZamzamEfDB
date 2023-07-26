namespace Zamzam.Core
{
    public class ReturnPurchaseOrder : BaseOrder
    {
        public Ulid SupplierId { get; set; }
        public Ulid EmployeeId { get; set; }
        public Ulid PurchaseId { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<ReturnPurchaseOrderLine> ReturnPurchaseOrderLines { get; set; }

    }
}
