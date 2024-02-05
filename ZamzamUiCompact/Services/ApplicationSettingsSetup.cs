namespace ZamzamUiCompact.Services;

public class ApplicationSettingsSetup(IConfiguration configuration) : IConfigureOptions<ApplicationSettings>
{
    private readonly IConfiguration _configuration = configuration;

    public void Configure(ApplicationSettings options)
    {
        _configuration.GetSection(nameof(ApplicationSettings)).Bind(options);
    }
}
