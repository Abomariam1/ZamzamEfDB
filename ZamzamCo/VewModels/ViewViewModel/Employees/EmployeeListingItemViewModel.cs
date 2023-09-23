using System.Windows.Input;

namespace ZamzamCo.VewModels.ViewViewModel.Employees
{
    public class EmployeeListingItemViewModel : ViewModelBase
    {
        //public Employee SelectedEmployee { get; }

        //public string EmpName => SelectedEmployee.Name;
        public ICommand AddCommand { get; }

        public EmployeeListingItemViewModel()
        {
            //SelectedEmployee = employee;
        }
    }
}
