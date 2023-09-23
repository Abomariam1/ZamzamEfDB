using ZamzamCo.Navigations;

namespace ZamzamCo.VewModels
{
    public class MainViewModel : ViewModelBase
    {
        public INavigator Navigator { get; set; }
        public ViewModelBase CurrentViewModel => Navigator.CurrentViewModel;
        public MainViewModel(INavigator navigator, ViewModelBase viewModel)
        {
            //navigator ? Navigator= new Navigator(): Navigator = navigator;
            if (navigator == null)
            {
                Navigator = new MainNavigator();
                return;
            }
            Navigator = navigator;
            Navigator.CurrentViewModel = viewModel;
        }

    }
}
