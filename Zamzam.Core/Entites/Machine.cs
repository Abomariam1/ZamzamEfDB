using Zamzam.Domain.Common;

namespace Zamzam.Domain.Entites
{
    public class Machine : BaseAuditableEntity
    {
        public string MachineName { get; set; }
        public string ProssesorId { get; set; }
        public string ProssedorUUID { get; set; }
        public string HardDriveID { get; set; }
    }
}
