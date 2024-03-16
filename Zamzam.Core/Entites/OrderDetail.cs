using Zamzam.Domain.Common;
using Zamzam.Domain.Common.Interfaces;

namespace Zamzam.Domain
{
    public class OrderDetail: BaseAuditableEntity, IOrderDetail
    {
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }
        public virtual Order? Order { get; set; }
        public virtual Item? Item { get; set; }
    }
}
