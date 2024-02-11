namespace ZamzamUiCompact.Views.Pages
{
    /// <summary>
    /// Interaction logic for AreasPage.xaml
    /// </summary>
    public partial class AreasPage : INavigableView<AreaViewModel>
    {
        public AreaViewModel ViewModel { get; }
        public AreasPage(AreaViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }


        private void DataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
