using Zamzam.Domain.Common;
using Zamzam.Domain.Entites;

namespace Zamzam.Domain
{
    public class Item: BaseAuditableEntity
    {
        public string Name { get; set; }
        public decimal PurchasingPrice { get; set; }
        public decimal SellingCashPrice { get; set; }
        public decimal InstallmentPrice { get; set; }
        public int Balance { get; set; }
        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
        public virtual ICollection<ItemOperation>? ItemOperations { get; set; }
    }
}
