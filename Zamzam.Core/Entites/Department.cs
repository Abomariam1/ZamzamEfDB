namespace Zamzam.Core
{
    public class Department : BaseEntity
    {

        public string DepName { get; set; } = string.Empty;
        public DateOnly CreatedOn { get; set; }
        public virtual ICollection<Employee>? Employees { get; }

    }
}
