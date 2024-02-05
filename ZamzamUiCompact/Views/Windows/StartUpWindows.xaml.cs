namespace ZamzamUiCompact.Views.Windows
{
    /// <summary>
    /// Interaction logic for StartUpWindows.xaml
    /// </summary>
    public partial class StartUpWindows
    {
        public StartUpWindowViewModel ViewModel { get; }
        public StartUpWindows(StartUpWindowViewModel viewModel)
        {
            SystemThemeWatcher.Watch(this);
            ViewModel = viewModel;
            DataContext = ViewModel;
            InitializeComponent();
        }

    }
}
