namespace ZamzamUiCompact.Helpers;

public class WindowHelper
{
    public static bool GetEnableWindowClosing(DependencyObject obj)
    {
        return (bool)obj.GetValue(EnableWindowClosingProperty);
    }

    public static void SetEnableWindowClosing(DependencyObject obj, bool value)
    {
        obj.SetValue(EnableWindowClosingProperty, value);
    }

    // Using a DependencyProperty as the backing store for EnableWindowClosing.  This enables animation, styling, binding, etc...
    public static DependencyProperty EnableWindowClosingProperty =
        DependencyProperty.RegisterAttached(
            "EnableWindowClosing",
            typeof(bool),
            typeof(WindowHelper),
            new PropertyMetadata(false, OnEnableWindowClosingChanged));

    private static void OnEnableWindowClosingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Window window)
        {
            window.Loaded += (s, e) =>
            {
                if (window.DataContext is IWindowEvent viewModel)
                {
                    viewModel.Close += () =>
                    {
                        window.Visibility = Visibility.Hidden;
                    };
                    window.Closing += (s, e) =>
                    {
                        e.Cancel = !viewModel.CanClose();

                    };
                }
            };
        }
    }
}
