namespace ZamzamUiCompact.Models;

public class AuthenticatedUser: Model
{
    public string Token { get; set; }
    public string UserName { get; set; }
    public DateTime Expiration { get; set; }

    //public AuthenticatedUser GetUser()
    //{
    //    if(File.Exists("user.json"))
    //    {
    //        string? str = File.ReadAllText("user.json");
    //        AuthenticatedUser user = JsonSerializer.Deserialize<AuthenticatedUser>(str)!;
    //        return FillUser(user);
    //    }
    //    return this;
    //}

    //private void WriteUser(AuthenticatedUser user)
    //{
    //    if(user == null) throw new UserNullExecption("null user");
    //    File.WriteAllText("user.json", JsonSerializer.Serialize(user));
    //}

    //private AuthenticatedUser FillUser(AuthenticatedUser user)
    //{
    //    Token = user.Token;
    //    IsPasswordRemmemberd = user.IsPasswordRemmemberd;
    //    IsAutoLogging = user.IsAutoLogging;
    //    UserName = user.UserName;
    //    Password = user.Password;
    //    LogedInAt = user.LogedInAt;
    //    Expiration = user.Expiration;
    //    return this;
    //}
}
