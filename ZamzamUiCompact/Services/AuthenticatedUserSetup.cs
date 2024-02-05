namespace ZamzamUiCompact.Services;

internal class AuthenticatedUserSetup(IConfiguration configuration) : IConfigureOptions<AuthenticatedUser>
{
    private readonly IConfiguration _configuration = configuration;

    public void Configure(AuthenticatedUser options)
    {
        _configuration.GetSection(nameof(AuthenticatedUser)).Bind(options);
    }
}
