using ZamzamUiCompact.Services.RepositoryServices.Inteface;

namespace ZamzamUiCompact.ViewModels.Pages;

public partial class PurchaseViewModel(IUnitOfWork unitOfWork): ObservableValidator
{
    const string purchaseController = "Purchase";
    const string supplierController = "Supplier";
    const string employeeController = "Employee";
    const string ItemController = "Item";

    [ObservableProperty] DateTime _orderDate;
    [ObservableProperty] decimal _totalPrice;
    [ObservableProperty] decimal _totalDiscount;
    [ObservableProperty] int _invoceType; // نوع الفاتورة {كاش,اجل}و
    [ObservableProperty] SupplierModel _supplier = new();
    [ObservableProperty] ObservableCollection<SupplierModel> _suppliers = [];
    [ObservableProperty] EmployeeModel _employee = new();
    [ObservableProperty] ObservableCollection<EmployeeModel> _employees = [];
    [ObservableProperty] ItemModel _item = new();
    [ObservableProperty] ObservableCollection<ItemModel> _items = new();
    [ObservableProperty] OrderDetailsModel order = new();
    [ObservableProperty] ObservableCollection<OrderDetailsModel> _orderDetails = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Total))]
    int _quantity;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Total))]
    decimal _price;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Total))]
    decimal _discount;
    public decimal Total => (Quantity * Price) - Discount;

    [RelayCommand]
    public void AddOrderDetails()
    {
        Order.ItemId = Item.ItemId;
        Order.ItemName = Item.ItemName;
        Order.Quantity = Quantity;
        Order.Price = Price;
        Order.Discount = Discount;
        Order.Total = Total;
        OrderDetails.Add(Order);

    }

    [RelayCommand]
    public async Task GetAll()
    {
        await GetAllSuppliers();
        await GetAllemployees();
        await GetAllItems();
    }

    [RelayCommand]
    public async Task AddInvoice()
    {
        ValidateAllProperties();
        if(!HasErrors)
        {
            var purchase = new PurchaseModel()
            {
                OrderDate = OrderDate,
                TotalPrice = TotalPrice,
                TotalDiscount = TotalDiscount,
                Details = OrderDetails.ToList(),
                EmployeeId = Employee.EmployeeId,
                InvoiceType = InvoceType == 0 ? "Cash" : "Installment"
            };
            var supps = await unitOfWork.Service<PurchaseModel>().GetAllAsync(purchaseController);

        }
    }

    private async Task GetAllSuppliers()
    {
        Result<List<SupplierModel>>? supps = await unitOfWork.Service<SupplierModel>().GetAllAsync(supplierController);
        Suppliers = new ObservableCollection<SupplierModel>(supps.Data);
    }

    private async Task GetAllemployees()
    {
        Result<List<EmployeeModel>>? emps = await unitOfWork.Service<EmployeeModel>().GetAllAsync($"{employeeController}/getall");
        Employees = new ObservableCollection<EmployeeModel>(emps.Data);
    }

    private async Task GetAllItems()
    {
        Result<List<ItemModel>>? itms = await unitOfWork.Service<ItemModel>().GetAllAsync(ItemController);
        Items = new ObservableCollection<ItemModel>(itms.Data);
    }


}
