using System.Windows.Input;
using ZamzamCo.Commands;

namespace ZamzamCo.VewModels
{
    public class NavigatDepartment : ViewModelBase, INavigator
    {
        private ViewModelBase _viewModelBase;
        public ViewModelBase CurrentViewModel { get => _viewModelBase; set { _viewModelBase = value; OnPropertyChanged(nameof(CurrentViewModel)); } }

        public ICommand UpdateCurrentViewModelCommand => new NavigateDepartmentCommand(this);
    }
}
