using ZamzamUiCompact.Services.RepositoryServices.Inteface;

namespace ZamzamUiCompact.ViewModels.Windows;

public partial class LoginWindowViewModel(
    IUnitOfWork unitOfWork,
    IConfiguration configuration,
    IOptionsSnapshot<AuthenticatedUser> user,
    IOptionsMonitor<AuthenticatedUser> monitor,
    IOptionsSnapshot<SignInSettingsOptions> settings,
    AuthenticatedUser auth,
    IServiceProvider provider): ObservableObject, IWindowEvent
{
    const string accountController = "Account";


    [ObservableProperty]
    private string _applicationTitle = "زمزم لفلاتر المياه";

    [ObservableProperty]
    private string _userName = string.Empty;

    [ObservableProperty]
    private string _passwordStr = string.Empty;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [ObservableProperty]
    private Dictionary<string, List<string>> _errors = [];

    [ObservableProperty]
    private bool _rememberPassword = false;

    [ObservableProperty]
    private bool _autoLoggin = false;

    [ObservableProperty]
    private bool _isFileExists;
    public Action Close { get; set; } = () => { };

    public void Initialize()
    {
        if(settings.Value.IsPasswordRemmemberd)
        {
            UserName = user.Value.UserName;
            PasswordStr = user.Value.UserName;
        }
    }


    [RelayCommand]
    public async Task SignIn()
    {

        try
        {
            if(!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(PasswordStr))
            {
                var usr = new
                {
                    UserName,
                    Password = PasswordStr
                };
                Result<AuthenticatedUser>? result = await unitOfWork.Service<AuthenticatedUser>().SendRequst($"{accountController}/login", usr);

                if(result.Succeeded)
                {
                    auth = result.Data;
                    SignInSettingsOptions? setting = new()
                    {
                        IsAutoLogging = AutoLoggin,
                        IsPasswordRemmemberd = RememberPassword,
                        LogedInAt = DateTime.Now,
                        Password = PasswordStr,
                    };

                    string newUser = JsonSerializer.Serialize(auth);
                    string newSettings = JsonSerializer.Serialize(setting);

                    UserSettingsOPtions newUserSettings = new()
                    {
                        User = auth,
                        UserSettings = setting
                    };

                    JsonSerializerOptions options = new() { WriteIndented = true, PropertyNameCaseInsensitive = true };
                    string? newFile = JsonSerializer.Serialize(newUserSettings, options);


                    string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "user.json");

                    var fileWathcer = new FileSystemWatcher(
                        Path.GetDirectoryName(filePath)!,
                        Path.GetFileName(filePath));
                    fileWathcer.Changed += (sender, e) =>
                    {
                        var root = (IConfigurationRoot)configuration;
                        root.Reload();
                        var serv = App.Current.Properties.Values.GetEnumerator();
                    };
                    fileWathcer.EnableRaisingEvents = true;
                    configuration["user"] = newUser;
                    configuration["usersettings"] = newSettings;

                    File.WriteAllText(filePath, newFile);

                    var modified = monitor.CurrentValue;
                    ShowMainWindow(provider);
                    CloseWindow();
                }
                else
                {
                    ErrorMessage = result.Message;
                    return;
                }

            }
            else
            {
                ErrorMessage = "يجب ادخال اسم المستخدم وكلمة المرور";
            }
        }
        catch(Exception e)
        {
            ErrorMessage = e.Message;
        }
    }
    public bool CanClose() => true;
    public void CloseWindow() => Close?.Invoke();
    private void ShowMainWindow(IServiceProvider provider)
    {

        MainWindow? window = provider.GetRequiredService<MainWindow>();
        //window.ViewModel.User = _user;
        window.Show();
    }
}
