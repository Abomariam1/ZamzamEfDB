namespace ZamzamUiCompact.ViewModels.Windows;

public partial class MainWindowViewModel : ObservableObject, IWindowEvent
{
    private readonly IServiceProvider _provider;

    [ObservableProperty]
    private string _applicationTitle = "زمزم لفلاتر المياه";

    [ObservableProperty]
    private ObservableCollection<object> _menuItems = new()
    {
        new NavigationViewItem()
        {
            Content = "الرئيسية",
            Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
            TargetPageType = typeof(DashboardPage)
        },
         new NavigationViewItem()
        {
            Content = "الاقسام",
            Icon = new SymbolIcon {Symbol = SymbolRegular.AlignTop24 },
            TargetPageType = typeof(DepartmentPage),

        },
        new NavigationViewItem()
        {
            Content = "الموظفين",
            Icon = new SymbolIcon { Symbol = SymbolRegular.Person24 },
            TargetPageType = typeof(EmployeePage)
        },
        new NavigationViewItem()
        {
            Content = "المناطق",
            Icon = new SymbolIcon { Symbol = SymbolRegular.DataArea24 },
            TargetPageType = typeof(AreasPage)
        },
    };

    [ObservableProperty]
    private ObservableCollection<object> _footerMenuItems = new()
    {
        new NavigationViewItem()
        {
            Content = "Settings",
            Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
            TargetPageType = typeof(SettingsPage)
        }
    };

    [ObservableProperty]
    private ObservableCollection<MenuItem> _trayMenuItems = new()
    {
        new MenuItem { Header = "Home", Tag = "tray_home" }
    };

    [ObservableProperty]
    private IAuthenticatedUser _user;

    public Action Close { get; set; } = () => { };

    public MainWindowViewModel(IServiceProvider provider, IAuthenticatedUser user)
    {
        _provider = provider;
        User = user;
    }

    [RelayCommand]
    public void Logout()
    {
        CloseWindow();
        ShowLoginWindow();
    }

    private void ShowLoginWindow()
    {
        LoginWindow? window = _provider.GetRequiredService<LoginWindow>();
        window.Show();
    }
    public bool CanClose() => true;
    public void CloseWindow() => Close?.Invoke();

}
