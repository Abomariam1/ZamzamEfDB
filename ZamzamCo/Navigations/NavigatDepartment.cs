using System.Windows.Input;
using ZamzamCo.Commands.NavigationCommands;
using ZamzamCo.VewModels;

namespace ZamzamCo.Navigations
{
    public class NavigatDepartment : ViewModelBase, INavigator
    {
        private ViewModelBase _viewModelBase;
        public ViewModelBase CurrentViewModel
        {
            get => _viewModelBase;
            set
            {
                _viewModelBase = value;
                OnPropertyChanged(nameof(CurrentViewModel));
                OnPropertyChanged(nameof(IsOpend));
            }
        }
        public bool IsOpend => CurrentViewModel != null;
        public ICommand UpdateCurrentSubViewModelCommand => new UpdateSubViewModelCommand(this);

    }
}
