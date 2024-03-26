namespace ZamzamUiCompact.Views.Pages;
/// <summary>
/// Interaction logic for RePurchasePage.xaml
/// </summary>
public partial class RePurchasePage: INavigableView<ReturnPurchasesViewModel>
{
    public ReturnPurchasesViewModel ViewModel { get; set; }
    public RePurchasePage(ReturnPurchasesViewModel viewModel)
    {
        InitializeComponent();
        ViewModel = viewModel;
        DataContext = this;
    }
}
