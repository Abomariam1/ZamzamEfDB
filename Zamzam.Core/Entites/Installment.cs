namespace Zamzam.Core
{
    public class Installment : BaseEntity
    {
        public Ulid OrderId { get; set; }
        public Ulid CustomerId { get; set; }
        public DateOnly PayedOn { get; set; }
        public decimal Value { get; set; }
        public Ulid EmployeeId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual SaleOrder SalesOrder { get; set; }
    }
}
