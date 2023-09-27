using Zamzam.Domain.Types;

namespace Zamzam.Domain.Common.Interfaces
{
    internal interface IOrder : IEntity
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal NetPrice { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public int EmployeeId { get; set; }
    }
}
