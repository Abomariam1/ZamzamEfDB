namespace Zamzam.Core
{
    public class MaintenanceOrderLine
    {
        public int Id { get; set; }
        public int MaintenanceOrderId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; } = 0;
        public decimal TotalPrice { get; set; }
    }
}
