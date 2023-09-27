using Zamzam.Domain.Common;
using Zamzam.Domain.Common.Interfaces;
using Zamzam.Domain.Types;

namespace Zamzam.Domain
{

    public class Order : BaseAuditableEntity, IOrder
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal NetPrice { get; set; }
        public OrderType OrderType { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }

    }
}
