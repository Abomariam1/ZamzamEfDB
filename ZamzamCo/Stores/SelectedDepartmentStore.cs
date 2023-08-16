using System;
using Zamzam.Core;

namespace ZamzamCo.Stores
{
    public class SelectedDepartmentStore
    {
        private Department? _selectedDepartment;
        public event Action SelectedEmployeeChanged;
        public Department SelectedEmployee { get => _selectedDepartment; set { _selectedDepartment = value; SelectedEmployeeChanged?.Invoke(); } }

    }
}
