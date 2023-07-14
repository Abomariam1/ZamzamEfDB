namespace Zamzam.Core
{
    public class Area : BaseEntity
    {
        public string Name { get; set; }
        public string Staion { get; set; }
        public string? Location { get; set; }
        public ICollection<Customer> Customers { get; set; }
    }
}
