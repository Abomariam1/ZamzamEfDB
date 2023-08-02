using System.Windows.Input;
using ZamzamCo.Commands;

namespace ZamzamCo.VewModels
{
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
        public ICommand UpdateCurrentViewModelCommand => new UpdateViewModelCommand(this);
    }
}
