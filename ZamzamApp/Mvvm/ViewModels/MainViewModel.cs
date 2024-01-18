namespace ZamzamApp.Mvvm.ViewModels;

public class MainViewModel : ViewModelBase
{
    private INavigationService _navigation;

    private AuthenticatedUser _user;
    public INavigationService Navigation
    {
        get => _navigation;
        set => SetProperty(ref _navigation, value);
    }
    public AuthenticatedUser User
    {
        get => _user;
        set => SetProperty(ref _user, value);
    }
    public RelayCommand NavigateToHomeCommand { get; set; }
    public RelayCommand NavigateToSignInCommand { get; set; }

    public MainViewModel(INavigationService navigation)
    {
        Navigation = navigation;
        NavigateToHomeCommand = new RelayCommand(() => { Navigation.NavigateTo<HomeViewModel>(); }, () => true);
        NavigateToSignInCommand = new RelayCommand(() => { Navigation.NavigateTo<SignInViewModel>(); }, () => true);
    }

}
