namespace ZamzamApp.Services;

public class NavigationService : ObservableObject, INavigationService
{
    private ViewModelBase _currentView;
    private readonly Func<Type, ViewModelBase> _viewModelFactory;

    public ViewModelBase CurrentView
    {
        get
        {
            return _currentView;
        }
        private set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }
    public NavigationService(Func<Type, ViewModelBase> viewModelFactory)
    {
        _viewModelFactory = viewModelFactory;
    }
    public ViewModelBase NavigateTo<TViewModel>() where TViewModel : ViewModelBase
    {
        ViewModelBase viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
        CurrentView = viewModel;
        return viewModel;
    }
}
