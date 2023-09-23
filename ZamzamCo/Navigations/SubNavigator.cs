using System.Windows.Input;
using ZamzamCo.Commands.NavigationCommands;
using ZamzamCo.VewModels;
namespace ZamzamCo.Navigations
{
    public class SubNavigator : ViewModelBase, INavigator
    {
        private ViewModelBase _viewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return _viewModel; }
            set
            {
                _viewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
                OnPropertyChanged(nameof(IsOpend));
            }
        }
        public bool IsOpend => CurrentViewModel != null;
        public ICommand UpdateCurrentSubViewModelCommand => new UpdateSubViewModelCommand(this);

    }
}
