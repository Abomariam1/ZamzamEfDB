using ZamzamUiCompact.Execptions;

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
        if (File.Exists("user.json"))
        {
            string? str = File.ReadAllText("user.json");
            AuthenticatedUser user = JsonSerializer.Deserialize<AuthenticatedUser>(str)!;
            return FillUser(user);
        }
        return this;
    }

    private void WriteUser(AuthenticatedUser user)
    {
        if (user == null) throw new UserNullExecption("null user");
        File.WriteAllText("user.json", JsonSerializer.Serialize(user));
    }

    private AuthenticatedUser FillUser(AuthenticatedUser user)
    {
        Token = user.Token;
        IsPasswordRemmemberd = user.IsPasswordRemmemberd;
        IsAutoLogging = user.IsAutoLogging;
        UserName = user.UserName;
        Password = user.Password;
        LogedInAt = user.LogedInAt;
        Expiration = user.Expiration;
        return this;
    }
}
