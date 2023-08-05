using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Zamzam.Core;
using Zamzam.EF;
using Zamzam.EF.Services;
using ZamzamCo.Commands.CrudCommands;

namespace ZamzamCo.VewModels.ViewViewModel
{
    public class MinDepartmentsViewModel : ViewModelBase
    {
        private IDataService<Department> DepartmentService = new GenericDataServices<Department>(new ZamzamDbContextFactory());
        private EmployeeDataServices EmployeeService = new(new ZamzamDbContextFactory());

        private string departmentName;
        private Department _selectedDepatment;
        private DateOnly createdOn;
        public string DeparmentName { get { return departmentName; } set { departmentName = value; OnPropertyChanged(nameof(DeparmentName)); } }
        public DateOnly CreatedOn { get { return createdOn; } set { createdOn = value; OnPropertyChanged(nameof(CreatedOn)); } }
        public bool CanAdd => !string.IsNullOrEmpty(DeparmentName);
        public ICommand? AddDeparmentCommand => new CrudComand(AddDeparmentAsync);
        public Department SelectedDpeartment { get { return _selectedDepatment; } set { _selectedDepatment = value; OnPropertyChanged(nameof(SelectedDpeartment)); } }
        public ObservableCollection<Department>? Departments => new(DepartmentService.GetAll());


        #region Methods
        public void AddDeparmentAsync()
        {
            if (string.IsNullOrEmpty(DeparmentName))
            {
                MessageBox.Show("برجاء ادخال اسم القسم");

                return;
            }
            Department dep = new()
            {
                Name = DeparmentName,
            };
            DepartmentService.CreateAsync(dep);

        }

        //private ObservableCollection<Employee> GetEmployeesInDepartment(string departmentId)
        //{
        //    Employees = EmployeeService.GetById<Employee>(departmentId);
        //}

        #endregion

    }
}
