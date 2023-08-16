using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Zamzam.Core;
using Zamzam.EF;
using ZamzamCo.Commands.CrudCommands;

namespace ZamzamCo.VewModels.ViewViewModel
{
    public class MinDepartmentsViewModel : ViewModelBase
    {
        private readonly IDataService<Department> DepartmentService = new GenericDataServices<Department>(new ZamzamDbContextFactory());

        private string departmentName;
        private Department _selectedDepatment;
        private DateOnly _createdOn;


        public ObservableCollection<Department>? Departments => new(DepartmentService.GetAll());
        public string DeparmentName { get { return departmentName; } set { departmentName = value; OnPropertyChanged(nameof(DeparmentName)); } }
        public DateOnly CreatedOn { get { return _createdOn; } set { _createdOn = value; OnPropertyChanged(nameof(CreatedOn)); } }
        public bool CanAdd => !string.IsNullOrEmpty(DeparmentName);
        public Department SelectedDpeartment { get { return _selectedDepatment; } set { _selectedDepatment = value; OnPropertyChanged(nameof(SelectedDpeartment)); } }
        public ICommand? AddDeparmentCommand => new CrudComand(AddDeparmentAsync);


        #region Methods
        public void AddDeparmentAsync()
        {
            if (string.IsNullOrEmpty(DeparmentName))
            {
                MessageBox.Show("برجاء ادخال اسم القسم");

                return;
            }
            Department dep = new();
            dep.DepName = DeparmentName;
            dep.CreatedOn = CreatedOn;
            DepartmentService.CreateAsync(dep);
            MessageBox.Show("تم ادخال القسم بنجاح");

        }

        //private ObservableCollection<Employee> GetEmployeesInDepartment(string departmentId)
        //{
        //    Employees = EmployeeService.GetById<Employee>(departmentId);
        //}

        #endregion

    }
}
