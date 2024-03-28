namespace Zamzam.Domain
{
    public class ReturnPurchaseOrder: Order
    {
        public int PurchaseId { get; set; }
        public int SupplierId { get; set; }
        public int InvoiceNumber { get; set; }
        public required string ReasonForReturn { get; set; } = string.Empty;
        public virtual PurchaseOrder? Purchase { get; set; }
        public virtual Supplier? Supplier { get; set; }
    }
}
