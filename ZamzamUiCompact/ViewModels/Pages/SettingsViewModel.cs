namespace ZamzamUiCompact.ViewModels.Pages;

public partial class SettingsViewModel : ObservableObject, INavigationAware
{
    private bool _isInitialized = false;

    [ObservableProperty]
    private string _appVersion = String.Empty;

    [ObservableProperty]
    private SystemTheme _currentTheme = SystemTheme.Unknown;

    public void OnNavigatedTo()
    {
        if (!_isInitialized)
            InitializeViewModel();
    }

    public void OnNavigatedFrom() { }

    private void InitializeViewModel()
    {
        CurrentTheme = (SystemTheme)ApplicationThemeManager.GetAppTheme();
        AppVersion = $"ZamzamUiCompact - {GetAssemblyVersion()}";

        _isInitialized = true;
    }

    private string GetAssemblyVersion()
    {
        return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString()
            ?? String.Empty;
    }

    [RelayCommand]
    private void OnChangeTheme(string parameter)
    {
        switch (parameter)
        {
            case "theme_light":
                if (CurrentTheme == SystemTheme.Light)
                    break;

                ApplicationThemeManager.Apply(ApplicationTheme.Light, WindowBackdropType.Acrylic);
                CurrentTheme = SystemTheme.Light;

                break;

            default:
                if (CurrentTheme == SystemTheme.Dark)
                    break;

                ApplicationThemeManager.Apply(ApplicationTheme.Dark, WindowBackdropType.Acrylic);
                CurrentTheme = SystemTheme.Dark;

                break;
        }
    }
}
