namespace ZamzamUiCompact.Views.Pages;

/// <summary>
/// Interaction logic for PurchasePage.xaml
/// </summary>
public partial class PurchasePage : INavigableView<PurchaseViewModel>
{
    public PurchaseViewModel ViewModel { get; }
    public PurchasePage(PurchaseViewModel viewModel)
    {
        InitializeComponent();
        ViewModel = viewModel;
        DataContext = this;
    }

}
