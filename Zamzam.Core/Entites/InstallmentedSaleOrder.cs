﻿namespace Zamzam.Domain
{
    public class InstallmentedSaleOrder : Order
    {
        public decimal Payed { get; set; }
        public decimal Remains { get; set; }
        public decimal InstallmentValue { get; set; }
        public int InstallmentPeriodInMonths { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Installment> Installments { get; set; }
    }
}
