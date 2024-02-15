namespace ZamzamUiCompact.Models;

public class DepartmentModel : Model
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; }
    public List<EmployeeModel> Employees { get; set; }

    public override int GetHashCode()
    {
        int hashCode = 7;
        hashCode = (hashCode * 19) + DepartmentId.GetHashCode();
        return hashCode;
    }
}
