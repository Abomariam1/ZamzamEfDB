namespace Zamzam.Core
{
    public class Area : BaseEntity
    {
        public required string Name { get; set; } = string.Empty;
        public required string Station { get; set; } = string.Empty;
        public string? Location { get; set; } = string.Empty;
        public virtual ICollection<Customer>? Customers { get; set; }
    }
}
