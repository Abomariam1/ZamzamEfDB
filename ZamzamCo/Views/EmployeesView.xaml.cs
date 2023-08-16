using System.Windows.Controls;
using Zamzam.Core;

namespace ZamzamCo.Views
{
    /// <summary>
    /// Interaction logic for EmployeesView.xaml
    /// </summary>
    public partial class EmployeesView : UserControl
    {
        public EmployeesView()
        {
            InitializeComponent();
        }

        private void EmpTreeView_SelectedItemChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            if (sender is TreeView)
            {
                var emp = e.NewValue as Employee;
                if (emp != null)
                {

                }
            }
        }

        private void EmpDepCbo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cbo = sender as ComboBox;
            if (cbo != null)
            {
                var d = cbo.SelectedIndex;

            }
        }

        private void EmpTreeView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lst = sender as ListView;
            if (lst != null)
            {
                var x = lst.SelectedIndex;

            }
        }

        //private void EmpDepCbo_DropDownOpened(object sender, EventArgs e) => new EmployeeViewModel().ShowAllDepartments();
    }
}
