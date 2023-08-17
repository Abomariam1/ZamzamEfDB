using System.Collections.ObjectModel;
using Zamzam.Core.Entites;

namespace Zamzam.Core
{
    public class Maintenance : BaseEntity
    {
        public int OrderId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime NextMaintenanceDate { get; set; }
        public DateTime LastMaintenanceDate { get; set; }
        public bool IsMaintained { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual SaleOrder SaleOrder { get; set; }
        public virtual Collection<MaintenanceDetail> MaintenanceDetails { get; set; }
    }
}
