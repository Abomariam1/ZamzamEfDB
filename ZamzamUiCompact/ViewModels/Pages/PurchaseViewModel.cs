using ZamzamUiCompact.Services.RepositoryServices.Inteface;

namespace ZamzamUiCompact.ViewModels.Pages;

public partial class PurchaseViewModel(IUnitOfWork unitOfWork): ObservableValidator
{
    const string purchaseController = "Purchase";
    const string supplierController = "Supplier";
    const string employeeController = "Employee";
    const string ItemController = "Item";
    private readonly DispatcherTimer _dispatcher = new()
    {
        Interval = TimeSpan.FromMilliseconds(100)
    };
    private static int counter = 0;


    [ObservableProperty] DateTime _orderDate = DateTime.Today;
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
    private decimal _total;

    public decimal Total
    {
        get
        {
            _total = (Quantity * Price) - Discount;
            OnPropertyChanged(nameof(TotalPrice));
            return _total;
        }
    }
    public decimal TotalPrice => OrderDetails.Sum(x => x.Total);

    [ObservableProperty] string _message;
    [ObservableProperty] bool _status = false;
    [ObservableProperty] InfoBarSeverity _saverty = InfoBarSeverity.Informational;
    [ObservableProperty] string _infoBarTitle = "رسالة";


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
            PurchaseModel? purchase = new()
            {
                OrderDate = OrderDate,
                TotalPrice = TotalPrice,
                TotalDiscount = TotalDiscount,
                Details = [.. OrderDetails],
                EmployeeId = Employee.EmployeeId,
                InvoiceType = InvoceType,
                SupplierId = Supplier.SupplierId,
            };
            Result<PurchaseModel>? result = await unitOfWork.Service<PurchaseModel>().AddAsync(purchaseController, purchase);
            if(result.Succeeded)
            {

                Validate(result.Message, "Success", InfoBarSeverity.Success);
                return;
            }
            Validate(result.Message, "Error", InfoBarSeverity.Error);

            //Result<List<PurchaseModel>>? supps = await unitOfWork.Service<PurchaseModel>().GetAllAsync(purchaseController);

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

    private void Validate(string message, string title, InfoBarSeverity saverty)
    {
        Message = message;
        InfoBarTitle = title;
        Status = true;
        Saverty = saverty;
        StartTimer();
    }
    private void StartTimer()
    {
        _dispatcher.Tick += _dispatcher_Tick;
        _dispatcher.Start();
    }
    private void _dispatcher_Tick(object? sender, EventArgs e)
    {
        counter++;
        if(counter == 15)
        {
            counter = 0;
            StopAutoProgress();
            _dispatcher.Tick -= _dispatcher_Tick;
        }
    }
    private void StopAutoProgress()
    {
        // Stop the timer when needed
        if(_dispatcher != null)
        {
            Status = false;
            _dispatcher.Stop();
        }
    }

}
