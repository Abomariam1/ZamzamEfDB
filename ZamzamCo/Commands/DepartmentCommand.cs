using ZamzamCo.VewModels;
using ZamzamCo.VewModels.ViewViewModel;

namespace ZamzamCo.Commands
{
    public class DepartmentCommand : CommandBase
    {

        INavigator _navigator;

        public DepartmentCommand(INavigator navigator)
        {
            _navigator = navigator;
        }
        public override void Execute(object? parameter)
        {
            if (parameter is NavigatePrimatives primatives)
            {
                switch (primatives)
                {
                    case NavigatePrimatives.MinDepartments:
                        _navigator.CurrentViewModel = new MinDepartmentsViewModel();
                        break;
                }
            }
        }
    }
}
