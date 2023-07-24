using System.Windows;
using System.Windows.Controls;

namespace ZamzamCo.AttachedProperties
{
    public class PasswordAttachedProperty
    {
        public static readonly DependencyProperty HasPasswordProperty = DependencyProperty
            .RegisterAttached("HasPassword",
            typeof(bool),
            typeof(PasswordAttachedProperty),
            new PropertyMetadata(true, OnPasswordChanged));

        private static void OnPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }
        public static void SetHasPassword(PasswordBox p) => p.SetValue(HasPasswordProperty, p.SecurePassword.Length > 0);
        public static bool GetHasPassword(PasswordBox passwordBox) => (bool)passwordBox.GetValue(HasPasswordProperty);


        public static readonly DependencyProperty MonitorPasswordProperty = DependencyProperty
            .RegisterAttached("MonitorPassword",
            typeof(bool),
            typeof(PasswordAttachedProperty),
            new PropertyMetadata(true, OnMonitorPasswordChanged));

        private static void OnMonitorPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }
        public static void SetMonitorPassword(PasswordBox p, bool value) => p.SetValue(MonitorPasswordProperty, value);
        public static bool GetMonitorPassword(PasswordBox p) => (bool)p.GetValue(MonitorPasswordProperty);
    }
}
