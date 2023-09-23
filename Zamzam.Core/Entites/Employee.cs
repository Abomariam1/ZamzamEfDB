using Zamzam.Domain.Common;

namespace Zamzam.Domain

{
    public class Employee : BaseAuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string? PostalCode { get; set; } = string.Empty;
        public long NationalId { get; set; }
        public string Titel { get; set; } = string.Empty;
        public DateTime HireDate { get; set; }
        public DateTime BirthDate { get; set; }
        public decimal Salary { get; set; }
        public string Qualification { get; set; } = string.Empty;
        public byte[]? Photo { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Installment> Installments { get; set; }
        public virtual ICollection<Area> Areas { get; set; }
    }
}
