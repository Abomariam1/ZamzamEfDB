namespace ZamzamApp.Helpers;
public static class PasswordBoxHelper
{
    private static readonly DependencyProperty BoundPasswordProperty =
        DependencyProperty.RegisterAttached("BoundPassword", typeof(string), typeof(PasswordBoxHelper),
            new FrameworkPropertyMetadata(string.Empty, OnBoundPasswordChanged));
    //public static string BoundPassword
    //{
    //    get => GetBoundPassword(BoundPasswordProperty);
    //    private set;
    //}
    public static string GetBoundPassword(DependencyObject obj)
    {
        return (string)obj.GetValue(BoundPasswordProperty);
    }

    public static void SetBoundPassword(DependencyObject obj, string value)
    {
        obj.SetValue(BoundPasswordProperty, value);
    }

    private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is PasswordBox passwordBox)
        {
            // Avoid recursive updating
            passwordBox.PasswordChanged -= PasswordBoxPasswordChanged;

            if (e.NewValue != null)
            {
                passwordBox!.Password = e.NewValue.ToString();
            }
            else
            {
                passwordBox!.Password = string.Empty;
            }

            passwordBox.PasswordChanged += PasswordBoxPasswordChanged;

        }
    }

    private static void PasswordBoxPasswordChanged(object sender, RoutedEventArgs e)
    {
        if (sender is PasswordBox passwordBox)
        {
            SetBoundPassword(passwordBox, passwordBox.Password);
            passwordBox.GetType().GetMethod("Select", BindingFlags.Instance | BindingFlags.NonPublic)?
                .Invoke(passwordBox, new object[] { passwordBox.Password.Length, 0 });
        }
    }
}