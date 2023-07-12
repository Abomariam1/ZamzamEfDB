namespace Zamzam.Core
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal PurchasingPrice { get; set; }
        public decimal sellingCashPrice { get; set; }
        public decimal InstallmentPrice { get; set; }
        public int Balance { get; set; }
        public virtual IList<SaleOrderLine> OrderLines { get; set; }
        public virtual IList<PurchaseOrderLine> PurchaseOrderLines { get; set; }
        public virtual IList<MaintenanceOrderLine> MaintenanceOrderLines { get; set; }

    }
}
