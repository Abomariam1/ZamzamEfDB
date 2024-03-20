using Zamzam.Domain.Common;
using Zamzam.Domain.Entites;

namespace Zamzam.Domain;

public class Supplier: BaseAuditableEntity
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public decimal Balance { get; set; }
    public virtual ICollection<PurchaseOrder>? PurchaseOrders { get; set; }
    public virtual ICollection<SupplierOperations>? SupplierOperations { get; set; }
}
