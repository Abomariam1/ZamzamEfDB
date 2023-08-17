using System.Collections.ObjectModel;

namespace Zamzam.Core.Entites
{
    public class SaleOrder : Order
    {
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Collection<Maintenance> Maintenances { get; set; }


    }
}
