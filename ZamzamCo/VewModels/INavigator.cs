using System.Windows.Input;

namespace ZamzamCo.VewModels
{

    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
        ICommand UpdateCurrentViewModelCommand { get; }

    }
}
