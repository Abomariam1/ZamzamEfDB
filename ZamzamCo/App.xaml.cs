using System.Windows;
using ZamzamCo.VewModels;

namespace ZamzamCo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {

            Window window = new MainWindow();
            window.DataContext = new MainViewModel();
            window.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            window.Show();
            base.OnStartup(e);
        }
    }
}
