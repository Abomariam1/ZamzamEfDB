namespace Zamzam.Core
{
    public class Installment
    {
        public int InstallmentId { get; set; }
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
/*
 customer may purcheses many products
employee collect many installment
custmoer pay monthly
 
 
 */