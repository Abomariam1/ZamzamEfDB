namespace ZamzamUiCompact.Models;

public class AuthenticatedUser : IAuthenticatedUser
{
    public string Token { get; set; } = string.Empty;
    public bool IsPasswordRemmemberd { get; set; }
    public bool IsAutoLogging { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTime LogedInAt { get; set; }
    public DateTime Expiration { get; set; }

    public AuthenticatedUser GetUser()
    {
        AuthenticatedUser user = null;
        string? str = string.Empty;
        if (File.Exists("user.json"))
        {
            str = File.ReadAllText("user.json");
            user = JsonSerializer.Deserialize<AuthenticatedUser>(str)!;
            return user;
        }
        File.WriteAllText("user.json", JsonSerializer.Serialize(user));
        return this;
    }
}
