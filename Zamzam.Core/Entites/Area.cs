namespace Zamzam.Core
{
    public class Area : BaseEntity
    {
        public string Name { get; set; }
        public string Station { get; set; }
        public string? Location { get; set; }
        public ICollection<Customer> Customers { get; set; }

        public override string ToString()
        {
            string txt = $"Name = {Name} ----  Station = {Station} --------- Location = {Location}";
            return txt;
        }
    }
}
