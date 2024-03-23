namespace ZamzamUiCompact.Views.Pages
{
    /// <summary>
    /// Interaction logic for SalePage.xaml
    /// </summary>
    public partial class SalePage: INavigableView<SalesViewModel>
    {
        public SalesViewModel ViewModel { get; }
        public SalePage(SalesViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();
        }
    }
}
