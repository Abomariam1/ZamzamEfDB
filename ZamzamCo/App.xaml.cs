using System.Windows;
using ZamzamCo.VewModels;

namespace ZamzamCo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public App()
        {

        }
        protected override void OnStartup(StartupEventArgs e)
        {
            //IDataService<Item> itemService = new GenericDataServices<Item>(new ZamzamDbContextFactory());
            //IDataService<Department> DepartmentService = new GenericDataServices<Department>(new ZamzamDbContextFactory());
            //_ = itemService.Delete(1);
            Window window = new MainWindow
            {
                DataContext = new MainViewModel(),
                MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight
            };
            //Window window = new SignIn
            //{
            //    //DataContext = new SignInVewModel()
            //};

            window.Show();
            base.OnStartup(e);
        }


    }
}

