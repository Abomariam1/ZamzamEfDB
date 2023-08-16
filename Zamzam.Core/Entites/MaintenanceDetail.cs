namespace Zamzam.Core
{
    public class MaintenanceDetail : BaseEntity
    {
        public int MaintenanceId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }
        public virtual Item Item { get; set; }
        public virtual Maintenance Maintenance { get; set; }
    }
}
