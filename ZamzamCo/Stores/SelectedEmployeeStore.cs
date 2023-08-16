using System;
using Zamzam.Core;

namespace ZamzamCo.Stores
{
    public class SelectedEmployeeStore
    {
        private Employee? _selectedEmployee;
        public event Action SelectedEmployeeChanged;
        public Employee SelectedEmployee { get => _selectedEmployee; set { _selectedEmployee = value; SelectedEmployeeChanged?.Invoke(); } }

    }
}
