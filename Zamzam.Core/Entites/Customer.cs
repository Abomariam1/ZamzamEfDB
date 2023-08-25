using Zamzam.Domain.Common;

namespace Zamzam.Domain
{
    public class Customer : BaseAuditableEntity
    {
        public string Name { get; set; } = "عميل افتراضي";
        public string? Phone { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public long NationalCardId { get; set; } = 0;
        public string? Notes { get; set; } = string.Empty;
        public bool IsProplem { get; set; } = false;
        public bool IsBlackList { get; set; } = false;
        public int AreaId { get; set; }
        public virtual required Area Area { get; set; }
        //public virtual Collection<SaleOrder> SaleOrders { get; set; }
        //public virtual Collection<InstallmentedSaleOrder> InstallmentedSaleOrders { get; set; }
    }
}
