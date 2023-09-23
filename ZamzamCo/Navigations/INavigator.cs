using System.Windows.Input;
using ZamzamCo.VewModels;

namespace ZamzamCo.Navigations
{

    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
        bool IsOpend { get; }
        ICommand UpdateCurrentSubViewModelCommand { get; }

    }
}
