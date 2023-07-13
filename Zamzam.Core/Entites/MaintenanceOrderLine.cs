namespace Zamzam.Core
{
    public class MaintenanceOrderLine : BaseOrderLine
    {
        public int MaintenanceId { get; set; }
        public virtual Item Item { get; set; }
        public virtual Maintenance Maintenance { get; set; }
    }
}
