using System.ComponentModel.DataAnnotations;

namespace Zamzam.Application.Security;
public class TokenRequest
{
    [Required]
    public string Token { get; set; }
    [Required]
    public string RefreshToken { get; set; }
}
