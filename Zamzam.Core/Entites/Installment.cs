namespace Zamzam.Core
{
    public class Installment : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public DateOnly PayedOn { get; set; }
        public decimal Value { get; set; }
        public Guid EmployeeId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual SaleOrder SalesOrder { get; set; }
    }
}
