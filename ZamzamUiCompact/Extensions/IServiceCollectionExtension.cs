using ZamzamUiCompact.Services.Security;

namespace ZamzamUiCompact.Extensions;

public static class IServiceCollectionExtension
{
    public static void AddAllServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddServices();
        services.AddViewModel();
        services.AddViews();
        services.AddHttpFactory(configuration);
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddHostedService<ApplicationHostService>();
        services.AddSingleton<IEncryption, Encryption>();
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<ISnackbarService, SnackbarService>();
        services.AddSingleton<IContentDialogService, ContentDialogService>();
        services.AddSingleton<IAuthenticatedUser, AuthenticatedUser>();
        services.AddSingleton<ApplicationSettings>();
        services.AddSingleton<AreaService>();
        services.ConfigureOptions<AuthenticatedUserSetup>();
        services.ConfigureOptions<ApplicationSettingsSetup>();

    }

    private static void AddHttpFactory(this IServiceCollection services, IConfiguration configuration)
    {
        string baseAdress = configuration.GetSection("ServerUrl").Value!;
        SecureString? retrievedToken = CredentialCache.DefaultNetworkCredentials.SecurePassword;

        services.AddHttpClient("", options =>
        {
            options.BaseAddress = new Uri($"{baseAdress}");
            options.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        services.AddHttpClient<StartUpWindowViewModel>(client =>
        {
            client.BaseAddress = new Uri($"{baseAdress}/Account");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        services.AddHttpClient<LoginWindowViewModel>(client =>
        {
            client.BaseAddress = new Uri($"{baseAdress}/Account");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        services.AddHttpClient<DepartmentViewModel>(client =>
        {
            client.BaseAddress = new Uri($"{baseAdress}/Department");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        services.AddHttpClient<EmployeeViewModel>(client =>
        {
            client.BaseAddress = new Uri($"{baseAdress}/Employee");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        services.AddHttpClient();
    }

    private static void AddViewModel(this IServiceCollection services)
    {
        services.AddSingleton<StartUpWindowViewModel>();
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<LoginWindowViewModel>();
        services.AddSingleton<DashboardViewModel>();
        services.AddSingleton<SettingsViewModel>();
        services.AddSingleton<DataViewModel>();
        services.AddSingleton<DepartmentViewModel>();
        services.AddSingleton<DepartmentModel>();
        services.AddSingleton<EmployeeViewModel>();
        services.AddSingleton<AreaViewModel>();
    }

    private static void AddViews(this IServiceCollection services)
    {
        services.AddSingleton<StartUpWindows>();
        services.AddSingleton<LoginWindow>();
        services.AddSingleton<MainWindow>();
        services.AddSingleton<DashboardPage>();
        services.AddSingleton<SettingsPage>();
        services.AddSingleton<DataPage>();
        services.AddSingleton<DepartmentPage>();
        services.AddSingleton<EmployeePage>();
        services.AddSingleton<AreasPage>();
    }

}
