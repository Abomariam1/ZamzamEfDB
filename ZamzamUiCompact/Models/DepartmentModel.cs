namespace ZamzamUiCompact.Models;

public class DepartmentModel
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; }
    public List<EmployeeModel> Employees { get; set; }
}
