using System.Collections.Generic;
using System.Collections.ObjectModel;
using ZamzamCo.Stores;
using ZamzamCo.VewModels.ViewViewModel.Employees;

namespace ZamzamCo.VewModels.ViewViewModel
{
    public class EmployeeListingViewModel : ViewModelBase
    {
        private static readonly string[] _constr;

        //private readonly EmployeeDataServices employeeDataServices = new(new ZamzamDbContextFactory());
        private EmployeeListingItemViewModel _selectedEmployeeListingItemViewModel;
        private ObservableCollection<EmployeeListingItemViewModel> _employeeListingItemViewModel;
        private readonly SelectedEmployeeStore _selectedEmployeeStore;

        public IEnumerable<EmployeeListingItemViewModel> EmployeeListingItemViewModel => _employeeListingItemViewModel;

        public EmployeeListingItemViewModel SelectedEmployeeListingItemViewModel
        {
            get { return _selectedEmployeeListingItemViewModel; }
            set
            {
                _selectedEmployeeListingItemViewModel = value;
                OnPropertyChanged(nameof(SelectedEmployeeListingItemViewModel));
                //_selectedEmployeeStore.SelectedEmployee = _selectedEmployeeListingItemViewModel?.SelectedEmployee;
            }
        }
        public EmployeeListingViewModel(SelectedEmployeeStore selectedEmployeeStore)
        {
            //var emps = employeeDataServices.GetEmployeesInDepartment();
            //var em = new ObservableCollection<EmployeeListingItemViewModel>();
            //foreach (var emp in emps)
            //{
            //    em.Add(new EmployeeListingItemViewModel(emp));
            //}
            //_employeeListingItemViewModel = em;
            //_selectedEmployeeStore = selectedEmployeeStore;
        }

    }
}
