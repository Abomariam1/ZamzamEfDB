using Zamzam.Domain.Common;
using Zamzam.Domain.Types;

namespace Zamzam.Domain.Entites;
public class ItemOperation: BaseAuditableEntity
{
    public int OrderId { get; set; }
    public int ItemId { get; set; }
    public int OldBalance { get; set; }
    public int NewBalance { get; set; }
    public int Value { get; set; }
    public OperationType OperationType { get; set; }
    public OrderType OrderType { get; set; }
    public virtual Item? Items { get; set; }
    public virtual Order? Order { get; set; }
}
