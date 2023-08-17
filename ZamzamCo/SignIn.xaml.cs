using System.Windows;
using System.Windows.Controls;
using Zamzam.Core;
using Zamzam.Core.Entites;
using Zamzam.EF;
using Zamzam.EF.Services;
using ZamzamCo.Navigations;

namespace ZamzamCo
{
    /// <summary>
    /// Interaction logic for SignIn.xaml
    /// </summary>
    public partial class SignIn : Window
    {
        readonly IDataService<User> userService = new UserDataService(new ZamzamDbContextFactory());
        public static INavigator Navigator => new MainNavigator();
        public SignIn()
        {

            InitializeComponent();
        }

        private async void SignInBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(UserTxt.Text)) { MessageBox.Show("ادخل اسم المستخدم"); return; }
            if (string.IsNullOrEmpty(PasswordTxt.Password)) { MessageBox.Show("ادخل كلمة المرور"); return; }
            User user = await userService.FindByNameAsync("Mostafa");
            if (user != null)
            {
                if (user.UserName == UserTxt.Text && user.Password == PasswordTxt.Password)
                {
                    Window window = new MainWindow
                    {
                        //DataContext = new MainViewModel(Navigator),
                        MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight
                    };
                    window.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("اسم المستخدم او كلمة المرور خطأ", "خطأ");
                }
            }
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("هل تريد الخروج من البرنامج", "خروج", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                Application.Current.Shutdown();
            }
        }

        private void CloseBtnCtrl_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void PasswordTxt_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }

        private void PasswordBoxTest_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (e.Source != null)
            {
                var pws = (PasswordBox)e.Source;
                var str = pws.Password;
            }
        }
    }
}
