namespace ZamzamUiCompact.Models;

public class DepartmentModel: Model
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; }
    public List<EmployeeModel> Employees { get; set; }

    public override bool Equals(object? obj)
    {
        if(obj == null) return false;
        if(ReferenceEquals(this, obj)) return true;
        if(obj is not DepartmentModel other) return false;
        if(ReferenceEquals(this, other)) return true;

        return DepartmentId == other.DepartmentId
            && DepartmentName == other.DepartmentName;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(DepartmentId, DepartmentName);
    }
}
