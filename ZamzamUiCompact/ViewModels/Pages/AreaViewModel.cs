namespace ZamzamUiCompact.ViewModels.Pages;

public partial class AreaViewModel : ObservableObject
{

    [ObservableProperty] private int _id;

    [ObservableProperty] private string _name = String.Empty;

    [ObservableProperty] private string _station = String.Empty;

    [ObservableProperty] private string _location = String.Empty;

    [ObservableProperty] private int _employeeId;

    [ObservableProperty] private string __employeeName = String.Empty;
}
