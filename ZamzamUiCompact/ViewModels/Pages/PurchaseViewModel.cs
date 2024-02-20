using ZamzamUiCompact.Services.RepositoryServices.Inteface;

namespace ZamzamUiCompact.ViewModels.Pages;

public partial class PurchaseViewModel(IUnitOfWork unitOfWork): ObservableObject
{
    const string purchaseController = "Purchase";
    const string supplierController = "Supplier";
    const string employeeController = "Employee";
    const string ItemController = "Item";
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    [ObservableProperty] DateTime _orderDate;
    [ObservableProperty] decimal _totalPrice;
    [ObservableProperty] decimal _totalDiscount;
    [ObservableProperty] string _invoceType = string.Empty; // نوع الفاتورة {كاش,اجل}و
    [ObservableProperty] SupplierModel _supplier = new();
    [ObservableProperty] ObservableCollection<SupplierModel> _suppliers = [];
    [ObservableProperty] EmployeeModel _employee = new();
    [ObservableProperty] ObservableCollection<EmployeeModel> _employees = [];
    [ObservableProperty] ItemModel _item = new();
    [ObservableProperty] ObservableCollection<ItemModel> _items = new();
    [ObservableProperty] ObservableCollection<OrderDetailsModel> _orderDetails = new();

    [ObservableProperty]
    decimal? _price;

    [ObservableProperty]
    decimal? _discount;

    [ObservableProperty]
    int? _quantity;

    [ObservableProperty()]
    readonly decimal _Total;

    [RelayCommand]
    public async Task GetAllSuppliers()
    {
        Result<List<SupplierModel>>? supps = await _unitOfWork.Service<SupplierModel>().GetAllAsync(supplierController);
        Suppliers = new ObservableCollection<SupplierModel>(supps.Data);
    }
    [RelayCommand]
    public async Task GetAllemployees()
    {
        Result<List<EmployeeModel>>? emps = await _unitOfWork.Service<EmployeeModel>().GetAllAsync(employeeController);
        Employees = new ObservableCollection<EmployeeModel>(emps.Data);
    }

    [RelayCommand]
    public async Task GetAllItems()
    {
        Result<List<ItemModel>>? itms = await _unitOfWork.Service<ItemModel>().GetAllAsync(ItemController);
        Items = new ObservableCollection<ItemModel>(itms.Data);
    }
    [RelayCommand]
    public void AddOrderDetails()
    {

    }

}
