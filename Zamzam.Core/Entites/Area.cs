namespace Zamzam.Core
{
    public class Area : BaseEntity
    {
        public required string Name { get; set; }
        public required string Station { get; set; }
        public string? Location { get; set; }
        public virtual ICollection<Customer>? Customers { get; set; }
    }
}
