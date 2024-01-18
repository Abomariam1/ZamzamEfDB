namespace ZamzamApp.Mvvm.Models;

public class AuthenticatedUser
{
    public string Token { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTime SignedAt { get; set; }
    public DateTime Expiration { get; set; }

}
