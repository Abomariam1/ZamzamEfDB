using ZamzamUiCompact.Services.RepositoryServices;
using ZamzamUiCompact.Services.RepositoryServices.Inteface;
using ZamzamUiCompact.Services.Security;

namespace ZamzamUiCompact.Extensions;

public static class IServiceCollectionExtension
{
    public static void AddAllServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddServices();
        services.AddConfiguration(configuration);
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
        services.AddSingleton<ApplicationSettings>();
        services.AddTransient(typeof(IGenericService<>), typeof(GenericService<>));
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddScoped<AuthenticatedUser>();
        //services.ConfigureOptions<AuthenticatedUserSetup>();
        //services.ConfigureOptions<ApplicationSettingsSetup>();

    }

    private static void AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AuthenticatedUser>(configuration.GetSection("User"));
        services.Configure<SignInSettingsOptions>(configuration.GetSection("UserSettings"));
        //services.AddTransient<IOptionsSnapshot<AuthenticatedUser>, OptionsManager<AuthenticatedUser>>();
        //services.AddTransient<IOptionsMonitor<AuthenticatedUser>, OptionsMonitor<AuthenticatedUser>>();
        //services.AddTransient<IOptionsSnapshot<SignInSettingsOptions>, OptionsManager<SignInSettingsOptions>>();
        //services.AddTransient<IOptionsMonitor<SignInSettingsOptions>, OptionsMonitor<SignInSettingsOptions>>();

        services.AddSingleton((IConfigurationRoot)configuration);

    }

    private static void AddHttpFactory(this IServiceCollection services, IConfiguration configuration)
    {
        string baseAdress = configuration.GetSection("ServerUrl").Value!;

        SecureString? retrievedToken = CredentialCache.DefaultNetworkCredentials.SecurePassword;

        services.AddHttpClient("services", options =>
        {
            options.BaseAddress = new Uri($"{baseAdress}");
            options.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        //services.AddHttpClient();
    }

    private static void AddViewModel(this IServiceCollection services)
    {
        services.AddSingleton<StartUpWindowViewModel>();
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<LoginWindowViewModel>();
        services.AddSingleton<DashboardViewModel>();
        services.AddSingleton<SettingsViewModel>();
        services.AddTransient<DepartmentViewModel>();
        services.AddTransient<DepartmentModel>();
        services.AddTransient<EmployeeViewModel>();
        services.AddTransient<AreaViewModel>();
        services.AddTransient<ItemsViewModel>();
        services.AddTransient<SupplierViewModel>();
        services.AddTransient<PurchaseViewModel>();
    }

    private static void AddViews(this IServiceCollection services)
    {
        services.AddSingleton<StartUpWindows>();
        services.AddSingleton<LoginWindow>();
        services.AddSingleton<MainWindow>();
        services.AddSingleton<DashboardPage>();
        services.AddSingleton<SettingsPage>();
        services.AddSingleton<DepartmentPage>();
        services.AddSingleton<EmployeePage>();
        services.AddSingleton<AreasPage>();
        services.AddSingleton<ItemsPage>();
        services.AddSingleton<SupplierPage>();
        services.AddSingleton<PurchasePage>();
    }

}
