namespace Zamzam.Core
{
    public abstract class BaseOrderLine : BaseEntity, IOrderLine
    {
        public Ulid OrderId { get; set; }
        public Ulid ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
