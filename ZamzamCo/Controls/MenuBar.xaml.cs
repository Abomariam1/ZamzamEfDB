using System.Windows;
using System.Windows.Controls;

namespace ZamzamCo.Controls
{
    /// <summary>
    /// Interaction logic for MenuBar.xaml
    /// </summary>
    public partial class MenuBar : UserControl
    {
        public MenuBar()
        {
            InitializeComponent();
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("هل تريد الخروج من البرنامج", "خروج", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                Application.Current.Shutdown();
            }
        }

        private void ExitRadiobutton_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var Rd = (RadioButton)sender;
            if (Rd != null)
            {

            }
        }
    }
}
