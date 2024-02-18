namespace ZamzamUiCompact.ViewModels.Pages;

public partial class PurchaseViewModel : ObservableObject
{
    [ObservableProperty] SupplierModel _supplier = new();
    [ObservableProperty] ObservableCollection<SupplierModel> _suppliers = [];
    [ObservableProperty] string _invoceType = string.Empty;
    [ObservableProperty] DateTime _date;

}
