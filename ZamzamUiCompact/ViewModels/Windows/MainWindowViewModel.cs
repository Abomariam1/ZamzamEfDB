namespace ZamzamUiCompact.ViewModels.Windows;

public partial class MainWindowViewModel(IServiceProvider provider, IOptionsSnapshot<AuthenticatedUser> options)
    : ObservableObject, IWindowEvent
{
    [ObservableProperty]
    private string _applicationTitle = "زمزم لفلاتر المياه";

    [ObservableProperty]
    private ObservableCollection<object> _menuItems =
    [
        new NavigationViewItem()
        {
            Content = "الرئيسية",
            Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
            TargetPageType = typeof(DashboardPage),

        },
        new NavigationViewItemSeparator(),
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
        new NavigationViewItem()
        {
            Content = "الاصناف",
            Icon = new SymbolIcon { Symbol = SymbolRegular.TrayItemAdd24 },
            TargetPageType = typeof(ItemsPage)
        },
        new NavigationViewItem()
        {
            Content = "الموردين",
            Icon = new SymbolIcon { Symbol = SymbolRegular.Sanitize24 },
            TargetPageType = typeof(SupplierPage)
        },
        new NavigationViewItem()
        {
            Content = "المشتريات",
            Icon = new SymbolIcon { Symbol = SymbolRegular.Production24 },
            TargetPageType = typeof(PurchasePage)
        },
        new NavigationViewItem()
        {
            Content = "مرتجعات المشتريات",
            Icon = new SymbolIcon { Symbol = SymbolRegular.ArrowRedo24 },
            TargetPageType = typeof(ReturnPurchasePage)
        },
        new NavigationViewItem()
        {
            Content = "المبيعات",
            Icon = new SymbolIcon { Symbol = SymbolRegular.Production24 },
            TargetPageType = typeof(SalePage)
        },
        new NavigationViewItem()
        {
            Content = "مرتجعات المبيعات",
            Icon = new SymbolIcon { Symbol = SymbolRegular.CaretDown24 },
            TargetPageType = typeof(ReturnSalesPage)
        },

    ];

    [ObservableProperty]
    private ObservableCollection<object> _footerMenuItems =
    [
        new NavigationViewItem()
        {
            Content = "Settings",
            Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
            TargetPageType = typeof(SettingsPage)
        }
    ];

    [ObservableProperty]
    private ObservableCollection<MenuItem> _trayMenuItems =
    [
        new MenuItem { Header = "Home", Tag = "tray_home" },
    ];

    [ObservableProperty]
    private AuthenticatedUser _user = options.Value;

    public Action Close { get; set; } = () => { };

    [RelayCommand]
    public void Logout()
    {
        CloseWindow();
        ShowLoginWindow();
    }

    private void ShowLoginWindow()
    {
        LoginWindow? window = provider.GetRequiredService<LoginWindow>();
        window.Show();
    }
    public bool CanClose() => true;
    public void CloseWindow() => Close?.Invoke();

}
