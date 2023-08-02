using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Zamzam.Core;
using ZamzamCo.Commands;

namespace ZamzamCo.VewModels.ViewViewModel
{
    public class EmployeeViewModel : ViewModelBase
    {
        private ObservableCollection<DepartmentViewModel> _departments;
        private string _empName;
        public ObservableCollection<DepartmentViewModel> Departments { get { return _departments; } set { _departments = value; OnPropertyChanged(nameof(Departments)); } }
        public string EmpName { get { return _empName; } set { _empName = value; OnPropertyChanged(nameof(EmpName)); } }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string? PostalCode { get; set; }
        public string Country { get; set; }
        public long NID { get; set; }
        public DepartmentViewModel SelectedDepartment { get; set; }
        public string Titel { get; set; }
        public DateOnly HireDate { get; set; }
        public DateOnly BirthDate { get; set; }
        public decimal Salary { get; set; }
        public string Qualifications { get; set; }
        public byte[]? Photo { get; set; }
        public INavigator SubNavigation { get; set; } = new NavigatDepartment();
        public ICommand AddEmployee { get; }
        public ICommand EditEmployee { get; }
        public ICommand DeleteEmployee { get; }
        public ICommand AddDepartmentCommand => new NavigateDepartmentCommand(SubNavigation);
        public ICommand AddPhoto { get; }
        //public static EmployeeViewModel SelectedEmployee = new EmployeeViewModel()
        //{
        //    EmpName = "Mosataf",
        //    Phone = "01066686997",
        //    City = "Kafr Sakr",
        //    Region = "Asharkia",
        //    Country = "Misr",
        //    BirthDate = new DateOnly(1985, 09, 28),
        //    HireDate = new DateOnly(2023, 7, 1),
        //    NID = 28509281301179,
        //    PostalCode = "44755",
        //    Salary = 60000,
        //    Titel = "Account Manager"
        //};
        public EmployeeViewModel()
        {
            _departments = new()
            {
                new DepartmentViewModel(new Department{Name = "الادارة"}),
                new DepartmentViewModel(new Department{Name = "الحسابات"}),
                new DepartmentViewModel(new Department{Name = "المبيعات"}),
                new DepartmentViewModel(new Department{Name = "التحصيل"}),
                new DepartmentViewModel(new Department{Name = "الصيانة"})
            };
            //var dp = Departments;

        }

    }
}

//_selectedEmployee = new()

