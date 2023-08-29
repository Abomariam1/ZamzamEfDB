using Zamzam.Domain.Common;

namespace Zamzam.Domain
{
    public class Installment : BaseAuditableEntity
    {
        public int OrderId { get; set; }
        public DateTime PayedOn { get; set; }
        public decimal Value { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual InstallmentedSaleOrder InstallmentedSale { get; set; }
    }
}
