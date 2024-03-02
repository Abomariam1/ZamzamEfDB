namespace Zamzam.Application.Security;
public class JwtConfig
{
    public string Secret { get; set; }
    public Uri ValidEssure { get; set; }
    public Uri ValidAudiance { get; set; }

}
