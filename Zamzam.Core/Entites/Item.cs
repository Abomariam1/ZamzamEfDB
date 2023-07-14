namespace Zamzam.Core
{
    public class Item : BaseEntity
    {
        public string Name { get; set; }
        public decimal PurchasingPrice { get; set; }
        public decimal sellingCashPrice { get; set; }
        public decimal InstallmentPrice { get; set; }
        public int Balance { get; set; }
        public virtual ICollection<SaleOrderLine> SaleOrderLines { get; set; }
        public virtual ICollection<PurchaseOrderLine> PurchaseOrderLines { get; set; }
        public virtual ICollection<MaintenanceOrderLine> MaintenanceOrderLines { get; set; }
        public virtual ICollection<ReturnPurchaseOrderLine> ReturnPurchaseOrderLines { get; set; }
        public virtual ICollection<ReturnSaleOrderLine> ReturnSaleOrderLines { get; set; }

    }
}
