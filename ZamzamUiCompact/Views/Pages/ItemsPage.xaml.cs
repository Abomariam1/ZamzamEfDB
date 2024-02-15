namespace ZamzamUiCompact.Views.Pages;

/// <summary>
/// Interaction logic for ItemsPage.xaml
/// </summary>
public partial class ItemsPage : INavigableView<ItemsViewModel>
{
    public ItemsViewModel ViewModel { get; }
    public ItemsPage(ItemsViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;
        InitializeComponent();
    }
}
