using ZamzamUiCompact.Services.RepositoryServices;
using ZamzamUiCompact.Services.RepositoryServices.Inteface;
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
        services.AddTransient(typeof(IGenericService<>), typeof(GenericService<>));
        services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));

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
            //options.DefaultRequestHeaders.Add("Authorization", _user.Token);
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
        services.AddHttpClient();
    }

    private static void AddViewModel(this IServiceCollection services)
    {
        services.AddSingleton<StartUpWindowViewModel>();
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<LoginWindowViewModel>();
        services.AddSingleton<DashboardViewModel>();
        services.AddSingleton<SettingsViewModel>();
        services.AddTransient<DataViewModel>();
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
        services.AddSingleton<DataPage>();
        services.AddSingleton<DepartmentPage>();
        services.AddSingleton<EmployeePage>();
        services.AddSingleton<AreasPage>();
        services.AddSingleton<ItemsPage>();
        services.AddSingleton<SupplierPage>();
        services.AddSingleton<PurchasePage>();
    }

}
