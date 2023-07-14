namespace Zamzam.Core
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Installment> Installments { get; set; }
        public virtual ICollection<SaleOrder> Sales { get; set; }
        public virtual ICollection<PurchaseOrder> Purchases { get; set; }
        public virtual ICollection<ReturnSaleOrder> ReturnSales { get; set; }
        public virtual ICollection<ReturnPurchaseOrder> ReturnPurchases { get; set; }
        public virtual ICollection<Maintenance> Maintenances { get; set; }
    }
}
