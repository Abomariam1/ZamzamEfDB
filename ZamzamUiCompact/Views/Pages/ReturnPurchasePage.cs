namespace ZamzamUiCompact.Views.Pages;

/// <summary>
/// Interaction logic for ReturnPurchasesPage.xaml
/// </summary>
public partial class ReturnPurchasePage: INavigableView<ReturnPurchasesViewModel>
{
    public ReturnPurchasesViewModel ViewModel { get; }
    public ReturnPurchasePage(ReturnPurchasesViewModel viewModel)
    {
        InitializeComponent();
        ViewModel = viewModel;
        DataContext = this;
    }
}
