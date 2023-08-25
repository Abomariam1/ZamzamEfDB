using Zamzam.Domain.Entites;

namespace Zamzam.Domain
{
    public class Maintenance : Order
    {
        public DateTime NextMaintenanceDate { get; set; }
        public DateTime LastMaintenanceDate { get; set; }
        public bool IsMaintained { get; set; }
        public int SaleOrderId { get; set; }
        public virtual SaleOrder SaleOrder { get; set; }
    }
}
