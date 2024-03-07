using Microsoft.Extensions.Configuration.UserSecrets;
using ZamzamUiCompact.Services.RepositoryServices.Inteface;

namespace ZamzamUiCompact.ViewModels.Windows;

public partial class LoginWindowViewModel(
    IUnitOfWork unitOfWork,
    IOptionsSnapshot<AuthenticatedUser> SnapUser,
    IOptionsMonitor<AuthenticatedUser> monitor,
    AuthenticatedUser auth,
    IServiceProvider provider): ObservableValidator, IWindowEvent
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


                    string dirPath = Assembly.GetExecutingAssembly().Location;
                    var filePath = Path.Combine(PathHelper.GetSecretsPathFromSecretsId("90b93fa4-7cb7-4f21-bb38-60199bfdb648"));
                    //var filePath2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "user.json");



                    File.WriteAllText(filePath, newFile);
                    //File.WriteAllText(filePath2, newFile);

                    //var conf = configuration as IConfigurationRoot;
                    //conf.Bind("User", auth);
                    //conf.Bind("UserSettings", setting);
                    //conf.Reload();
                    var su = SnapUser.Value;
                    var su2 = monitor.CurrentValue;
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
        window.ViewModel.User = SnapUser.Value;
        window.Show();
    }


}
