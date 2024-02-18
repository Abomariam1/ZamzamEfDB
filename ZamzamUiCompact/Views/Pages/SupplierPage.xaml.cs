namespace ZamzamUiCompact.Views.Pages;

/// <summary>
/// Interaction logic for SupplierPage.xaml
/// </summary>
public partial class SupplierPage : INavigableView<SupplierViewModel>
{
    public SupplierPage(SupplierViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;
        InitializeComponent();
    }

    public SupplierViewModel ViewModel { get; }
}
