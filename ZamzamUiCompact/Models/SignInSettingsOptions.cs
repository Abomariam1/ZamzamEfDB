namespace ZamzamUiCompact.Models;
public class SignInSettingsOptions
{
    public bool IsPasswordRemmemberd { get; set; }
    public bool IsAutoLogging { get; set; }
    public DateTime LogedInAt { get; set; }
    public string Password { get; set; } = string.Empty;
}
