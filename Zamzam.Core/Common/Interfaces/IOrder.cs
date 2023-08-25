using Zamzam.Domain.Types;

namespace Zamzam.Domain.Common.Interfaces
{
    internal interface IOrder : IEntity
    {
        public DateOnly OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Payed { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal Remains { get; set; }
        public InvoiceType InvoiceType { get; set; }
    }
}
