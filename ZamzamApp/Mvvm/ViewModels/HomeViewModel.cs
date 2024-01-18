namespace ZamzamApp.Mvvm.ViewModels;

public class HomeViewModel : ViewModelBase
{
    private INavigationService _navigation;
    private readonly HttpClient _httpClientFactory;
    public INavigationService Navigation
    {
        get => _navigation;
        set
        {
            _navigation = value;
            OnPropertyChanged();
        }
    }
    public RelayCommand NavigateToSignInCommand { get; set; }
    public RelayCommand NavigateToHomeCommand { get; set; }
    public HomeViewModel(INavigationService navigation, IHttpClientFactory httpClientFactory)
    {
        Navigation = navigation;
        NavigateToHomeCommand = new RelayCommand(() => Navigation.NavigateTo<HomeViewModel>());
        NavigateToSignInCommand = new RelayCommand(() => Navigation.NavigateTo<SignInViewModel>());
        _httpClientFactory = httpClientFactory.CreateClient();
    }
    public async Task NavigatToHome()
    {
        Navigation.NavigateTo<HomeViewModel>();
    }
}
