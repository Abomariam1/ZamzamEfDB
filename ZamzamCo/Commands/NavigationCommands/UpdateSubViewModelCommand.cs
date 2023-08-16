using ZamzamCo.Navigations;
using ZamzamCo.VewModels.ViewViewModel;

namespace ZamzamCo.Commands.NavigationCommands
{
    public class UpdateSubViewModelCommand : CommandBase
    {

        INavigator _navigator;

        public UpdateSubViewModelCommand(INavigator navigator)
        {
            _navigator = navigator;
        }
        public override void Execute(object? parameter)
        {
            if (parameter is NavigatePrimativesType primatives)
            {
                switch (primatives)
                {
                    case NavigatePrimativesType.MinDepartments:
                        _navigator.CurrentViewModel = new MinDepartmentsViewModel();
                        break;
                }
            }
        }
    }
}
