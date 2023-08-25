using System.Collections.ObjectModel;
using Zamzam.Domain.Common;

namespace Zamzam.Domain.Entites
{
    public class SaleOrder : BaseAuditableEntity
    {
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Collection<Maintenance> Maintenances { get; set; }


    }
}
