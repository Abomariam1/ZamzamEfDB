using Zamzam.Domain.Common;

namespace Zamzam.Domain
{
    public class Area : BaseAuditableEntity
    {
        public required string Name { get; set; } = string.Empty;
        public required string Station { get; set; } = string.Empty;
        public string? Location { get; set; } = string.Empty;
        public int EmployeeId { get; set; }
        public virtual ICollection<Customer>? Customers { get; set; }
        public virtual Employee? Employee { get; set; }
    }
}
