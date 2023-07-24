using System.Windows;
using Zamzam.Core;
using Zamzam.Core.Entites;
using Zamzam.EF;
using ZamzamCo.VewModels;

namespace ZamzamCo
{
    /// <summary>
    /// Interaction logic for SignIn.xaml
    /// </summary>
    public partial class SignIn : Window
    {
        IDataService<User> userService = new GenericDataServices<User>(new ZamzamDbContextFactory());
        string pstext;
        public SignIn()
        {

            InitializeComponent();
        }

        private void SignInBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(UserTxt.Text)) { MessageBox.Show("ادخل اسم المستخدم"); return; }
            if (string.IsNullOrEmpty(PasswordTxt.Password)) { MessageBox.Show("ادخل كلمة المرور"); return; }
            User user = userService.GetById(2).Result;
            if (user != null)
            {
                if (user.UserName == UserTxt.Text && user.Password == PasswordTxt.Password)
                {
                    Window window = new MainWindow
                    {
                        DataContext = new MainViewModel(),
                        MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight
                    };
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

        //private void PasswordTxt_PasswordChanged(object sender, RoutedEventArgs e)
        //{
        //    var p = (PasswordBox)sender;
        //    pstext = p.Password;
        //}
    }
}
