using Zamzam.Core.Entites;

namespace Zamzam.Core
{
    public class Item : BaseEntity
    {
        public string Name { get; set; }
        public decimal PurchasingPrice { get; set; }
        public decimal sellingCashPrice { get; set; }
        public decimal InstallmentPrice { get; set; }
        public int Balance { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<MaintenanceDetail> MaintenanceDetails { get; set; }

    }
}
