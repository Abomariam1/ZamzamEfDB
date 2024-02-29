namespace Zamzam.Application.Security;
public class AuthResult
{
    public string Token { get; set; }
    public bool Success { get; set; }
    public string Error { get; set; }
}
