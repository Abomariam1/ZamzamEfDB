namespace Zamzam.Domain.Common.Interfaces
{
    public interface IOrderDetail
    {
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
