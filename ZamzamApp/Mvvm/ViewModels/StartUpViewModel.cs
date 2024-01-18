using ZamzamApp.Validations;

namespace ZamzamApp.Mvvm.ViewModels;
public partial class StartUpViewModel : ObservableObject, IWindowEvent
{
    private readonly IApiHelper _apiHelper;
    private readonly INavigationService _navigationService;
    private string _userName = string.Empty;
    private string _password = string.Empty;
    private bool _isSignedIn = false;
    private string _errorMeessage = string.Empty;
    private AuthenticatedUser _AuthenticatedUser;
    private Dictionary<string, List<string>> _errors;
    public string UserName
    {
        get => _userName;
        set => SetProperty(ref _userName, value);
    }
    [Bindable(true, BindingDirection.TwoWay)]
    public string PasswordStr { get; set; }
    public bool IsSignedIn
    {
        get => _isSignedIn;
        set => SetProperty(ref _isSignedIn, value);
    }
    public string ErrorMessage
    {
        get
        {
            if (Errors != null)
            {
                foreach (var val in Errors.AsEnumerable())
                {
                    var newValue = val.Value;
                    return string.Join(",", newValue);
                }
            }
            return _errorMeessage;
        }
        set => SetProperty(ref _errorMeessage, value);

    }
    public Dictionary<string, List<string>> Errors
    {
        get => _errors;
        set
        {
            SetProperty(ref _errors, value);


        }
    }

    public Action Close { get; set; } = () => { };

    public StartUpViewModel(IApiHelper apiHelper, INavigationService navigationService)
    {
        _apiHelper = apiHelper;
        _navigationService = navigationService;
    }

    [RelayCommand]
    public async Task SignIn()
    {
        try
        {
            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(PasswordStr))
            {
                AuthenticatedUser user = new()
                { UserName = this.UserName, Password = this.PasswordStr };

                HttpResponseMessage? auth = await _apiHelper.PostModelAsync("/Account/login", user);

                if (auth.IsSuccessStatusCode)
                {
                    AuthenticatedUser? result = await auth.Content.ReadAsAsync<AuthenticatedUser>();
                    ShowWindow(result);
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
                }
            }

        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
        }

    }

    //private async Task<AuthenticatedUser> Authinticate()
    //{
    //    AuthenticatedUser? user = null;
    //    if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(PasswordStr))
    //    {
    //        user = await _apiHelper.Authenticate(UserName, PasswordStr);
    //        IsSignedIn = true;
    //    }
    //    return user;
    //}

    public bool CanClose() => true;

    public void CloseWindow() => Close.Invoke();

    public void ShowWindow(AuthenticatedUser user)
    {
        MainWindow? window = new MainWindow();
        MainViewModel? mainViewModel = (MainViewModel?)_navigationService.NavigateTo<MainViewModel>();
        mainViewModel.User = user;
        window.DataContext = mainViewModel;
        window.Show();
    }
}
