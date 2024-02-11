namespace ZamzamUiCompact.Services;

public class AuthenticatedUserSetup(IConfiguration configuration) : IConfigureOptions<AuthenticatedUser>
{
    private readonly IConfiguration _configuration = configuration;

    public void Configure(AuthenticatedUser options)
    {
        if (_configuration.GetSection(nameof(AuthenticatedUser)).Value == null)
        {
            _configuration["Users"] = JsonSerializer.Serialize(options);
        }
        _configuration.GetSection(nameof(AuthenticatedUser)).Bind(options);
    }
}
