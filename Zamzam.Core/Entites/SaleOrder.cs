using ZamzamEfDb.Test.Entites;
using ZamzamEfDb.Test.Entites.Interfaces;

namespace Zamzam.Core
{
    public class SaleOrder : BaseOrder
    {
        //public virtual Employee Employee { get; set; }
        public virtual IList<SaleOrderLine> SaleOrderLines { get; set; }
        public virtual IList<Installment> Installments { get; set; }

    }
}
