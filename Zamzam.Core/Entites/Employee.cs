namespace Zamzam.Core
{
    public class Employee : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public virtual Department Department { get; set; }
        public virtual IList<Installment> Installments { get; set; }
        public virtual IList<SaleOrder> Sales { get; set; }
        public virtual IList<PurchaseOrder> Purchases { get; set; }
        public virtual IList<ReturnSaleOrder> ReturnSales { get; set; }
        public virtual IList<ReturnPurchaseOrder> ReturnPurchases { get; set; }
        public virtual IList<Maintenance> Maintenances { get; set; }

    }
}
