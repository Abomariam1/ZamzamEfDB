namespace Zamzam.Core
{
    public class Department : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<Employee> Employees { get; }
    }
}
