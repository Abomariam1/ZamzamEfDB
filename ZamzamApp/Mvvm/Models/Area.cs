namespace ZamzamApp.Mvvm.Models;

public class Area
{
    public required string Name { get; set; } = string.Empty;
    public required string Station { get; set; } = string.Empty;
    public string? Location { get; set; } = string.Empty;
    public int EmployeeId { get; set; }
}
