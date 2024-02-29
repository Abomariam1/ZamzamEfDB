using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Zamzam.Application.DTOs;
public class UserRegistrationDto
{
    [Required]
    [EmailAddress]
    public string UserName { get; set; }
    [Required]
    [PasswordPropertyText]
    public string Password { get; set; }
}
