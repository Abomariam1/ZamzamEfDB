using System.Windows.Controls;
using System.Windows.Input;

namespace ZamzamUiCompact.Views.Pages;

/// <summary>
/// Interaction logic for DepartmentPage.xaml
/// </summary>
public partial class DepartmentPage : INavigableView<DepartmentViewModel>
{


    public ICommand SelectionChangedCommand
    {
        get { return (ICommand)GetValue(SelectionChangedCommandProperty); }
        set { SetValue(SelectionChangedCommandProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty SelectionChangedCommandProperty =
        DependencyProperty.Register("SelectionChangedCommand", typeof(ICommand), typeof(ListView), new PropertyMetadata(null));


    public DepartmentViewModel ViewModel { get; }
    public DepartmentPage(DepartmentViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;
        InitializeComponent();
    }

}
