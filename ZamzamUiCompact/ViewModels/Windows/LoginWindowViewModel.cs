using ZamzamUiCompact.Services.RepositoryServices.Inteface;

namespace ZamzamUiCompact.ViewModels.Windows;

public partial class LoginWindowViewModel(
    IUnitOfWork unitOfWork,
    IConfiguration configuration,
    IOptionsMonitor<AuthenticatedUser> monitorUser,
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

    //public void Initialize()
    //{
    //    if(settingsOption.Value.IsPasswordRemmemberd)
    //    {
    //        UserName = user.Value.UserName;
    //        PasswordStr = user.Value.UserName;
    //    }
    //}


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

                    JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true, WriteIndented = true };
                    string? newFile = JsonSerializer.Serialize(newUserSettings, options);


                    string dirPath = AppDomain.CurrentDomain.BaseDirectory;
                    var filePath = Path.Combine(App.FindProjectPath(dirPath), "user.json");
                    var filePath2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "user.json");
                    //string filePath = "/Settings / user.json";

                    //var fileWathcer = new FileSystemWatcher(
                    //    Path.GetDirectoryName(filePath)!,
                    //    Path.GetFileName(filePath));
                    //fileWathcer.Changed += FileWathcer_Changed;
                    //fileWathcer.Created += FileWathcer_Created;
                    //fileWathcer.EnableRaisingEvents = true;


                    File.WriteAllText(filePath, newFile);
                    File.WriteAllText(filePath2, newFile);

                    var conf = configuration as IConfigurationRoot;
                    conf.Bind("User", auth);
                    conf.Bind("UserSettings", setting);
                    conf.Reload();

                    ShowMainWindow(provider);
                    CloseWindow();
                    //Restarting the program
                    //string appPath = Process.GetCurrentProcess().MainModule.FileName;
                    //Process.Start(appPath);
                    //Environment.Exit(0);
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
        window.ViewModel.User = monitorUser.CurrentValue;
        window.Show();
    }


}
