namespace Zamzam.Application.DTOs;
public class UserDto
{
    public string Token { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public DateTime? Expiration { get; set; }
}
