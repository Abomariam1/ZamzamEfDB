namespace ZamzamUiCompact.Views.Pages;

/// <summary>
/// Interaction logic for DepartmentPage.xaml
/// </summary>
public partial class DepartmentPage : INavigableView<DepartmentViewModel>
{
    public DepartmentViewModel ViewModel { get; }
    public DepartmentPage(DepartmentViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;
        InitializeComponent();
    }
}
