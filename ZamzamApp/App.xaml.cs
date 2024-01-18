namespace ZamzamApp;
public sealed partial class App : Application
{
    private readonly IHost _appHost;
    private readonly HostApplicationBuilder builder;

    App()
    {
        builder = Host.CreateApplicationBuilder();
        _appHost = ConfigureServices(builder);
    }
    protected override void OnStartup(StartupEventArgs e)
    {
        StartUpWindow _startUpForm = _appHost.Services.GetService<StartUpWindow>()!;
        _startUpForm.Show();
        _appHost.Start();
        base.OnStartup(e);
    }
    protected override void OnExit(ExitEventArgs e)
    {
        _appHost.StopAsync();
        _appHost?.Dispose();
        base.OnExit(e);
    }
    private static IHost ConfigureServices(HostApplicationBuilder builder)
    {
        builder.Services.AddAllServices(builder.Configuration);
        return builder.Build();
    }

}
