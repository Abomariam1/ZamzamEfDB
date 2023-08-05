namespace Zamzam.Core
{
    public class ReturnPurchaseOrder : BaseOrder
    {
        public Guid SupplierId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid PurchaseId { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<ReturnPurchaseOrderLine> ReturnPurchaseOrderLines { get; set; }

    }
}
