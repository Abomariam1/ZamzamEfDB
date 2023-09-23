using System.Windows;
using ZamzamCo.Controls;
using ZamzamCo.Dialogs;
using ZamzamCo.Navigations;
using ZamzamCo.Stores;
using ZamzamCo.VewModels;
using ZamzamCo.VewModels.ViewViewModel;

namespace ZamzamCo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {


        private readonly INavigator _navigation;
        private readonly SelectedEmployeeStore _selectedEmployeeStore;
        public App()
        {
            _navigation = new MainNavigator();
            DialogService.RegisterDialog<MinDepartments, MinDepartmentsViewModel>();
            _selectedEmployeeStore = new SelectedEmployeeStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            EmployeeViewModel viewModel = new(_selectedEmployeeStore);
            Window window = new MainWindow
            {
                DataContext = new MainViewModel(_navigation, viewModel),
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

