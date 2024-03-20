using Zamzam.Domain.Entites;

namespace Zamzam.Domain;

public class PurchaseOrder: Order
{
    public int InvoiceNumber { get; set; }
    public int SupplierId { get; set; }
    public virtual Supplier Supplier { get; set; }
    public virtual ICollection<SupplierOperations> SupplierOperations { get; set; }
}
