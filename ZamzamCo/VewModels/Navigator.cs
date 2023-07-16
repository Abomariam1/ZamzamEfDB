using System.Windows.Input;

namespace ZamzamCo.VewModels
{
    public enum ViewType
    {
        Home,
        Items,
        Sales,
        Installments
    }
    public class Navigator : ViewModelBase, INavigator
    {
        private ViewModelBase _viewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return _viewModel; }
            set
            {
                _viewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }
        public ICommand UpdateCurrentViewModelCommand => new UpdateViewModel(this);


    }
}
