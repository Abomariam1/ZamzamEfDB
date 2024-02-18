namespace ZamzamUiCompact.Validations;

public class InfoBarService
{
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public InfoBarSeverity Severity { get; set; }
    public bool IsOpen { get; set; }
    public bool IsVisable { get; set; }
}
