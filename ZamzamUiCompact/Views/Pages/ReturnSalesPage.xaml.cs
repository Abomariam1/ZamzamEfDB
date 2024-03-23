namespace ZamzamUiCompact.Views.Pages
{
    /// <summary>
    /// Interaction logic for ReturnSalesPage.xaml
    /// </summary>
    public partial class ReturnSalesPage: INavigableView<ReturnSalesViewModel>
    {
        public ReturnSalesViewModel ViewModel { get; }
        public ReturnSalesPage(ReturnSalesViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();
        }
    }
}
