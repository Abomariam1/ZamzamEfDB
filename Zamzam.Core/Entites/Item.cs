using Zamzam.Domain.Common;

namespace Zamzam.Domain
{
    public class Item : BaseAuditableEntity
    {
        public string Name { get; set; }
        public decimal PurchasingPrice { get; set; }
        public decimal sellingCashPrice { get; set; }
        public decimal InstallmentPrice { get; set; }
        public int Balance { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

    }
}
