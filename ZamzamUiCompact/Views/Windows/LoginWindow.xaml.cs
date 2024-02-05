namespace ZamzamUiCompact.Views.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow
    {
        public LoginWindowViewModel ViewModel { get; }
        public LoginWindow(LoginWindowViewModel viewModel)
        {
            SystemThemeWatcher.Watch(this);

            ViewModel = viewModel;
            DataContext = ViewModel;
            InitializeComponent();

        }
    }
}
