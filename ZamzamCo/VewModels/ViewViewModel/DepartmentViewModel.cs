using System.Windows.Input;
using Zamzam.Core;

namespace ZamzamCo.VewModels.ViewViewModel
{
    public class DepartmentViewModel : ViewModelBase
    {
        private string departmentName;
        public string DeparmentName
        {
            get { return departmentName; }
            set
            {
                departmentName = value;
                OnPropertyChanged(nameof(DeparmentName));
                //OnPropertyChanged(nameof(CanAdd));
            }
        }
        //bool CanAdd => !string.IsNullOrEmpty(DeparmentName);
        public ICommand? AddDeparmentCommand { get; }
        public DepartmentViewModel(Department department)
        {
            DeparmentName = department.Name;
        }

        public DepartmentViewModel()
        {
        }
    }
}
