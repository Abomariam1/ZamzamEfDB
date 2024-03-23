using Zamzam.Domain.Common;
using Zamzam.Domain.Types;

namespace Zamzam.Domain.Entites;
public class SupplierOperations: BaseAuditableEntity
{
    public DateTime OperationDate { get; set; }
    public int SupplierId { get; set; }
    public int OrderId { get; set; }
    public decimal OldBalance { get; set; } = decimal.Zero;
    public OperationType OperationType { get; set; }
    public decimal Value { get; set; }
    public decimal NewBalance { get; set; } = decimal.Zero;
    public virtual Supplier? Supplier { get; set; }
    public virtual Order? Order { get; set; }

}
