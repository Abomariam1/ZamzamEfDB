namespace Zamzam.Core
{
    public interface IOrderLine
    {
        public Ulid OrderId { get; set; }
        public Ulid ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
