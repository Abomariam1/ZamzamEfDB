using System.Windows;
using System.Windows.Controls;

namespace ZamzamCo.Controls
{
    /// <summary>
    /// Interaction logic for BindablePassowrdBox.xaml
    /// </summary>
    public partial class BindablePassowrdBox : UserControl
    {


        // Using a DependencyProperty as the backing store for PasswordProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password",
                typeof(string), typeof(BindablePassowrdBox),
                new PropertyMetadata(string.Empty, OnPasswordChanged));

        public static string GetMyProperty(DependencyObject obj)
        {
            return (string)obj.GetValue(PasswordProperty);
        }

        public static void SetMyProperty(DependencyObject obj, string value)
        {
            obj.SetValue(PasswordProperty, value);
        }

        private static void OnPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }
        public string Password
        {
            get => (string)GetValue(PasswordProperty);
            set => SetValue(PasswordProperty, value);
        }

        public BindablePassowrdBox()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = passwordBox.Password;
        }
    }

}
