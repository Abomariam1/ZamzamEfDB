namespace ZamzamUiCompact.ViewModels.Windows;

public partial class LoginWindowViewModel : ObservableObject, IWindowEvent
{
    private const string _userPath = "user.json";
    private readonly HttpClient _httpClient;
    private readonly IServiceProvider _serviceProvider;
    private IAuthenticatedUser _user;

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

    public LoginWindowViewModel(HttpClient httpClient,
        IServiceProvider serviceProvider,
        IAuthenticatedUser user)
    {
        _httpClient = httpClient;
        _serviceProvider = serviceProvider;
        _user = user.GetUser() ?? new();
        Initialize();
    }

    public void Initialize()
    {
        if (!string.IsNullOrEmpty(_user?.UserName))
        {
            if (_user.IsPasswordRemmemberd)
            {
                UserName = _user.UserName;
                PasswordStr = _user.Password;
                RememberPassword = true;
            }
        }
    }

    [RelayCommand]
    public async Task SignIn()
    {
        try
        {
            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(PasswordStr))
            {
                _user.UserName = UserName;
                _user.Password = PasswordStr;
                HttpResponseMessage? auth = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}/login", _user);

                if (auth.IsSuccessStatusCode)
                {
                    var content = await auth.Content.ReadAsStringAsync();

                    var _user = await auth.Content.ReadFromJsonAsync<AuthenticatedUser>(new JsonSerializerOptions()
                    {
                        IncludeFields = false,
                        PropertyNameCaseInsensitive = true
                    });
                    string token = _user.Token;
                    _user.Token = string.Empty;
                    _user.Token += $"Bearer {token}";
                    _user.IsPasswordRemmemberd = RememberPassword;
                    _user.IsAutoLogging = AutoLoggin;
                    _user.LogedInAt = DateTime.Now;
                    string? file = JsonSerializer.Serialize(_user);
                    File.WriteAllText(_userPath, file);
                    ShowMainWindow(_serviceProvider);
                    CloseWindow();
                }
                else
                {
                    string? errors = await auth.Content.ReadAsStringAsync();
                    Errors = ApiValidation.Validate(errors);
                    foreach (var error in Errors)
                    {
                        ErrorMessage = error.Value.FirstOrDefault()!;
                    }
                    return;
                }

            }
            else
            {
                ErrorMessage = "يجب ادخال اسم المستخدم وكلمة المرور";
            }
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
        }
    }
    public bool CanClose() => true;
    public void CloseWindow() => Close?.Invoke();
    public void ShowMainWindow(IServiceProvider provider)
    {
        MainWindow? window = provider.GetRequiredService<MainWindow>();
        window.ViewModel.User = _user;
        window.Show();
    }
}
