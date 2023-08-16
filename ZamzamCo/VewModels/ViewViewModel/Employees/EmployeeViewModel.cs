using ZamzamCo.Stores;

namespace ZamzamCo.VewModels.ViewViewModel
{
    public class EmployeeViewModel : ViewModelBase
    {
        public EmployeeDetailsViewModel EmployeeDetailsViewModel { get; }
        public EmployeeListingViewModel EmployeeListingViewModel { get; }

        public EmployeeViewModel(SelectedEmployeeStore _selectedEmployeeStore)
        {
            EmployeeDetailsViewModel = new(_selectedEmployeeStore);
            EmployeeListingViewModel = new(_selectedEmployeeStore);
        }

    }
}
