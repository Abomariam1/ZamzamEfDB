using System;
using System.Windows.Input;
using ZamzamCo.VewModels.ViewViewModel;

namespace ZamzamCo.VewModels
{
    public class UpdateViewModel : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private INavigator _navigation;
        public UpdateViewModel(INavigator navigator)
        {
            _navigation = navigator;
        }
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is ViewType)
            {
                ViewType type = (ViewType)parameter;
                switch (type)
                {
                    case ViewType.Home:
                        _navigation.CurrentViewModel = new HomeViewModel();
                        break;
                    case ViewType.Items:
                        _navigation.CurrentViewModel = new ItemViewModel();
                        break;
                }
            }
        }
    }
}
