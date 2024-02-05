
namespace ZamzamUiCompact.Models;

public interface IAuthenticatedUser
{
    DateTime Expiration { get; set; }
    bool IsAutoLogging { get; set; }
    bool IsPasswordRemmemberd { get; set; }
    DateTime LogedInAt { get; set; }
    string Password { get; set; }
    string Token { get; set; }
    string UserName { get; set; }

    public AuthenticatedUser GetUser();
}