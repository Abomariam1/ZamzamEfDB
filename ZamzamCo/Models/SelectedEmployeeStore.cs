using System;
using ZamzamCo.VewModels.ViewViewModel;

namespace ZamzamCo.Models
{
    public class SelectedEmployeeStore
    {
        private EmployeeViewModel? _employee;

        public SelectedEmployeeStore()
        {
            Employee.EmpName = "Mostafa";
        }

        public EmployeeViewModel Employee
        {
            get => _employee;
            set
            {
                _employee = value;
                SelectedEmployeeStoreChanged?.Invoke();
            }
        }

        public event Action SelectedEmployeeStoreChanged;
    }
}
