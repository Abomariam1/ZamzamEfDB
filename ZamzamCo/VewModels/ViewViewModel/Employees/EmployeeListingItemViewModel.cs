using System.Windows.Input;
using Zamzam.Core;

namespace ZamzamCo.VewModels.ViewViewModel.Employees
{
    public class EmployeeListingItemViewModel : ViewModelBase
    {
        public Employee SelectedEmployee { get; }

        public string EmpName => SelectedEmployee.Name;
        public ICommand AddCommand { get; }

        public EmployeeListingItemViewModel(Employee employee)
        {
            SelectedEmployee = employee;
        }
    }
}
