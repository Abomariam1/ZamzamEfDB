namespace ZamzamApp.Mvvm.ViewModels;
public partial class SignInViewModel : ViewModelBase
{
    private INavigationService _navigation;
    private string _userName;
    private string _password;

    public INavigationService Navigation
    {
        get => _navigation;
        set => SetProperty(ref _navigation, value);
    }
    public bool IsSignedIn { get; set; }
    public bool IsSignedOut { get; set; }
    public bool CanSignIn { get; set; }
    public string UserName
    {
        get => _userName;
        set
        {
            _userName = value;
            OnPropertyChanged();
        }
    }

    public string PasswordStr
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged();
        }
    }
    //public RelayCommandBase NavigateToHomeView {  get; set; }


    public SignInViewModel(INavigationService navigation)
    {
        Navigation = navigation;

    }

    public void SignOut()
    {

    }

    [RelayCommand]
    public void SignIn()
    {
        Navigation.NavigateTo<HomeViewModel>();
    }
}
