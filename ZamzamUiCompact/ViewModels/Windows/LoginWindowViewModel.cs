using ZamzamUiCompact.Services.RepositoryServices.Inteface;

namespace ZamzamUiCompact.ViewModels.Windows;

public partial class LoginWindowViewModel(
    IUnitOfWork unitOfWork,
    IConfiguration configuration,
    IOptionsSnapshot<AuthenticatedUser> user,
    IOptionsSnapshot<SignInSettingsOptions> settings,
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
    private Dictionary<string, List<string>> _errors = new();

    [ObservableProperty]
    private bool _rememberPassword = false;

    [ObservableProperty]
    private bool _autoLoggin = false;

    [ObservableProperty]
    private bool _isFileExists;
    public Action Close { get; set; }

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
                    AuthenticatedUser? _user = result.Data;
                    SignInSettingsOptions? setting = new SignInSettingsOptions
                    {
                        IsAutoLogging = AutoLoggin,
                        IsPasswordRemmemberd = RememberPassword,
                        LogedInAt = DateTime.Now,
                        Password = PasswordStr,
                    };
                    _user.Token = $"Bearer {_user.Token}";

                    string newUser = JsonSerializer.Serialize(_user);
                    string newSettings = JsonSerializer.Serialize(setting);

                    UserSettingsOPtions optin = new()
                    {
                        User = _user,
                        UserSettings = setting
                    };

                    string? newFile = JsonSerializer.Serialize(optin,
                        new JsonSerializerOptions { WriteIndented = true, PropertyNameCaseInsensitive = true });
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "user.json");

                    File.WriteAllText(filePath, newFile);
                    ShowMainWindow(provider);
                    CloseWindow();
                }
                else
                {
                    ErrorMessage = result.Message;
                    //Errors = ApiValidation.Validate(result.Message);
                    //foreach(var error in Errors)
                    //{
                    //    ErrorMessage = error.Value.FirstOrDefault()!;
                    //}
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
