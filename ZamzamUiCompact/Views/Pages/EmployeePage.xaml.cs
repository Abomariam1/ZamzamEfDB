using System.Windows.Controls;
using System.Windows.Input;

namespace ZamzamUiCompact.Views.Pages
{
    /// <summary>
    /// Interaction logic for EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage: Page
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

            if(e.NewValue is EmployeeModel emp)
            {
                ViewModel.SelectedEmployee = emp;
                ViewModel.FillProperities();
            }
        }

        private void autoBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            var ky = sender as AutoSuggestBox;
            if(ky != null)
            {
                var txt = args.Text;
            }
        }

        private void autoBox_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape) { return; }
            if(e.Key == Key.Enter)
            {
                if(sender is AutoSuggestBox suggestBox)
                {
                    EmployeeModel? emp = ViewModel.Employees.Where(x => x.EmployeeName == suggestBox.Text).FirstOrDefault();
                    ViewModel.SelectedEmployee = emp == null ? ViewModel.SelectedEmployee : emp;
                    ViewModel.FillProperities();
                }

            }
        }
    }
}
