using System.ComponentModel.DataAnnotations.Schema;
using Zamzam.Domain.Common;

namespace Zamzam.Application.Security;
public class RefreshToken: BaseAuditableEntity
{
    public string UserId { get; set; }
    public string Token { get; set; }
    public string JwtId { get; set; }
    public bool IsUsed { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime AddedDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public string NewToken { get; set; }

    [ForeignKey(nameof(UserId))]
    public ApplicationUser User { get; set; }
}
