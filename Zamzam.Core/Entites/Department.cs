using Zamzam.Domain.Common;

namespace Zamzam.Domain
{
    public class Department : BaseAuditableEntity
    {

        public string DepName { get; set; } = string.Empty;
        public DateOnly CreatedOn { get; set; }
        public virtual ICollection<Employee>? Employees { get; }

    }
}
