using System.Windows;
using ZamzamCo.Controls;
using ZamzamCo.Dialogs;
using ZamzamCo.VewModels;
using ZamzamCo.VewModels.ViewViewModel;

namespace ZamzamCo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
            DialogService.RegisterDialog<MinDepartments, MinDepartmentsViewModel>();
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

