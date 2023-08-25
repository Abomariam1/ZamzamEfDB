using System.Collections.ObjectModel;

namespace Zamzam.Domain
{
    public class InstallmentedSaleOrder : Order
    {
        public decimal InstallmentValue { get; set; }
        public int InstallmentPeriodInMonths { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Collection<Installment> Installments { get; set; }

    }
}
