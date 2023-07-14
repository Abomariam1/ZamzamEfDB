namespace Zamzam.Core
{
    public class Installment : BaseEntity
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateOnly PayedOn { get; set; }
        public decimal Value { get; set; }
        public int EmployeeId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual SaleOrder SalesOrder { get; set; }
    }
}
