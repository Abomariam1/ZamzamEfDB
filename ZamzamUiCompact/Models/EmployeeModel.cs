namespace ZamzamUiCompact.Models;

public class EmployeeModel: Model
{
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
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
    public string? Qualification { get; set; } = string.Empty;
    public string? Photo { get; set; }
    public int DepartmentId { get; set; }
    public DepartmentModel? Department { get; set; }

    public override int GetHashCode()
    {
        int hashCode = 23;
        hashCode = (hashCode * 19) + EmployeeId.GetHashCode();
        hashCode = (hashCode * 19) + EmployeeName.GetHashCode();
        hashCode = (hashCode * 19) + Phone.GetHashCode();
        hashCode = (hashCode * 19) + Address.GetHashCode();
        hashCode = (hashCode * 19) + City.GetHashCode();
        hashCode = (hashCode * 19) + Region.GetHashCode();
        hashCode = (hashCode * 19) + Country.GetHashCode();
        hashCode = (hashCode * 19) + (PostalCode == null ? 0 : PostalCode.GetHashCode());
        hashCode = (hashCode * 19) + NationalId.GetHashCode();
        hashCode = (hashCode * 19) + Titel.GetHashCode();
        hashCode = (hashCode * 19) + HireDate.GetHashCode();
        hashCode = (hashCode * 19) + BirthDate.GetHashCode();
        hashCode = (hashCode * 19) + Salary.GetHashCode();
        hashCode = (hashCode * 19) + (Qualification == null ? 0 : Qualification.GetHashCode());
        hashCode = (hashCode * 19) + (Photo == null ? 0 : Photo.GetHashCode());
        return hashCode;
    }
}
