namespace Zamzam.Core
{
    public class Maintenance
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public DateTime NextMaintenanceDate { get; set; }
        public DateTime LastMaintenanceDate { get; set; }
        public bool IsMaintained { get; set; }
        public int EmployeeId { get; set; }

    }
}
