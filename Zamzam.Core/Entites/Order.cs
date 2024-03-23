using Zamzam.Domain.Common;
using Zamzam.Domain.Common.Interfaces;
using Zamzam.Domain.Entites;
using Zamzam.Domain.Types;

namespace Zamzam.Domain
{

    public class Order: BaseAuditableEntity, IOrder
    {
        public DateTime OrderDate { get; set; } //تاريخ العملية
        public decimal TotalPrice { get; set; } // اجمالي السعر
        public decimal TotalDiscount { get; set; } // اجمالي الخصم
        public decimal TotalPayed { get; set; } // اجمالي المدفوع
        public decimal TotalRemaining { get; set; } // اجمالي المتبقي
        public decimal NetPrice { get; set; } // صافي السعر
        public OrderType OrderType { get; set; } // نوع العملية {بيع, شراء, مرتجع مبيعات, مرتجع مشتريات } و
        public InvoiceType InvoiceType { get; set; } // نوع الفاتورة {كاش,اجل} و
        public int EmployeeId { get; set; } // الموظف المسؤول عن العملية
        public virtual Employee Employee { get; set; }
        public virtual required ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Maintenance>? Maintenances { get; set; }
        public virtual ICollection<ItemOperation>? ItemOperations { get; set; }
        public virtual ICollection<SupplierOperations> SupplierOperations { get; set; }
    }
}
