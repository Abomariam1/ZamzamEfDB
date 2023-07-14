namespace Zamzam.Core
{
    public class ReturnPurchaseOrder : BaseOrder
    {
        public int SupplierId { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        //public virtual PurchaseOrder PurchaseOrder { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<ReturnPurchaseOrderLine> ReturnPurchaseOrderLines { get; set; }

    }
}
