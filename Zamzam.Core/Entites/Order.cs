using System.Collections.ObjectModel;
using Zamzam.Core.Types;

namespace Zamzam.Core.Entites
{

    public class Order : BaseEntity, IOrder
    {
        public DateOnly OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal NetPrice { get; set; }
        public decimal Payed { get; set; }
        public decimal Remains { get; set; }
        public OrderType OrderType { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Collection<OrderDetail> OrderDetails { get; set; }

    }
}
