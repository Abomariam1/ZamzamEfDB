namespace ZamzamUiCompact.Views.Pages;

/// <summary>
/// Interaction logic for ReturnPurchasesPage.xaml
/// </summary>
public partial class ReturnPurchasesPage: INavigableView<ReturnPurchasesViewModel>
{
    public ReturnPurchasesViewModel ViewModel { get; }
    public ReturnPurchasesPage(ReturnPurchasesViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;
        InitializeComponent();
    }
}
