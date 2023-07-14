namespace Zamzam.Core
{
    public class MaintenanceOrderLine : BaseOrderLine
    {
        public virtual Maintenance Maintenance { get; set; }
        public virtual Item Item { get; set; }
    }
}
