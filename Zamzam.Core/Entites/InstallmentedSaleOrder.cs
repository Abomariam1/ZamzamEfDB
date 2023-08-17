using System.Collections.ObjectModel;
using Zamzam.Core.Entites;

namespace Zamzam.Core
{
    public class InstallmentedSaleOrder : SaleOrder
    {
        public decimal InstallmentValue { get; set; }
        public int InstallmentPeriodInMonths { get; set; }
        public virtual Collection<Installment> Installments { get; set; }

    }
}
