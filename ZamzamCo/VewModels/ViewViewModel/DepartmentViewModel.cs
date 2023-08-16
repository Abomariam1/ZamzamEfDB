using System.Windows.Input;
using Zamzam.Core;

namespace ZamzamCo.VewModels.ViewViewModel
{
    public class DepartmentViewModel : ViewModelBase
    {
        private string departmentName;
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(nameof(Id)); }
        }
        public string DeparmentName
        {
            get { return departmentName; }
            set
            {
                departmentName = value;
                OnPropertyChanged(nameof(DeparmentName));
                OnPropertyChanged(nameof(CanAdd));
            }
        }
        bool CanAdd => !string.IsNullOrEmpty(DeparmentName);
        public ICommand? AddDeparmentCommand { get; }
        public DepartmentViewModel(Department department)
        {
            Id = department.Id;
            DeparmentName = department.DepName;
        }

        public DepartmentViewModel()
        {
        }
    }
}
