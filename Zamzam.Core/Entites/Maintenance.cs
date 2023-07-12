namespace Zamzam.Core
{
    public class Maintenance
    {
        public int Id { get; set; }
        public int SaleOrderId { get; set; }
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime NextMaintenanceDate { get; set; }
        public DateTime LastMaintenanceDate { get; set; }
        public bool IsMaintained { get; set; }
        public virtual IList<MaintenanceOrderLine> MaintenanceOrderLines { get; set; }

    }
}
