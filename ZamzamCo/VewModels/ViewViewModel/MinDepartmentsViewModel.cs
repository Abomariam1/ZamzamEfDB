using System;
using System.Windows.Input;
using Zamzam.Core;
using Zamzam.EF;
using ZamzamCo.Commands.CrudCommands;

namespace ZamzamCo.VewModels.ViewViewModel
{
    class MinDepartmentsViewModel : ViewModelBase
    {
        private IDataService<Department> DepartmentService = new GenericDataServices<Department>(new ZamzamDbContextFactory());

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
        private DateOnly createdOn;
        public DateOnly CreatedOn
        {
            get { return createdOn; }
            set
            {
                createdOn = value;
                OnPropertyChanged(nameof(CreatedOn));
                //OnPropertyChanged(nameof(CanAdd));
            }
        }
        //bool CanAdd => !string.IsNullOrEmpty(DeparmentName);
        public ICommand? AddDeparmentCommand => new CrudComand(AddDeparmentAsync);

        #region Methods
        public void AddDeparmentAsync()
        {
            Department dep = new()
            {
                Name = DeparmentName
            };
            DepartmentService.CreateAsync(dep);
        }
        #endregion



    }
}
