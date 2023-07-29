namespace Zamzam.Core
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; } = "عميل افتراضي";
        public string? Phone { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public long NationalCardId { get; set; } = 0;
        public string? Notes { get; set; } = string.Empty;
        public Ulid AreaId { get; set; }
        public bool IsProplem { get; set; } = false;
        public bool IsBlackList { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public virtual Area Area { get; }
        public virtual ICollection<SaleOrder> SaleOrders { get; set; }
        public virtual ICollection<Installment> Installments { get; set; }
        public virtual ICollection<Maintenance> Maintenances { get; set; }
        //public virtual ICollection<ReturnSaleOrder> ReturnSaleOrders { get; set; }

    }
}
