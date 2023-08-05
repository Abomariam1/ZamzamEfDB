namespace Zamzam.Core
{
    public class Maintenance : BaseEntity
    {
        public Guid SaleOrderId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime NextMaintenanceDate { get; set; }
        public DateTime LastMaintenanceDate { get; set; }
        public bool IsMaintained { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual SaleOrder SaleOrder { get; set; }
        public virtual ICollection<MaintenanceOrderLine> MaintenanceOrderLines { get; set; }
    }
}
