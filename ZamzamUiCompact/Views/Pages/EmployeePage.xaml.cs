using System.Windows.Controls;

namespace ZamzamUiCompact.Views.Pages
{
    /// <summary>
    /// Interaction logic for EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage : Page
    {
        public EmployeePage(EmployeeViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();
        }

        public EmployeeViewModel ViewModel { get; }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

            if (e.NewValue is EmployeeModel emp)
            {
                ViewModel.SelectedEmployee = emp;
                ViewModel.FillProperities();
            }
        }
    }
}
