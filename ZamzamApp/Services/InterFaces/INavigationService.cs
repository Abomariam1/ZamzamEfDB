namespace ZamzamApp.Services.InterFaces
{
    public interface INavigationService
    {
        ViewModelBase CurrentView { get; }
        ViewModelBase NavigateTo<TViewModel>() where TViewModel : ViewModelBase;
    }
}
