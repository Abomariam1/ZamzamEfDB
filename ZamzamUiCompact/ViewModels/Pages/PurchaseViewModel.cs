using ZamzamUiCompact.Services.RepositoryServices.Inteface;

namespace ZamzamUiCompact.ViewModels.Pages;

public partial class PurchaseViewModel(IUnitOfWork unitOfWork): BaseValidator
{
    const string purchaseController = "Purchase";
    const string supplierController = "Supplier";
    const string employeeController = "Employee";
    const string ItemController = "Item";

    [ObservableProperty]
    //[RegularExpression(@" ^ [0 - 9] * $")]
    [NotifyPropertyChangedFor(nameof(Total))]
    int _quantity;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Total))]
    decimal _price;

    [ObservableProperty]
    //[RegularExpression(@" ^ [0 - 9] * $")]
    [NotifyPropertyChangedFor(nameof(Total))]
    decimal _discount;
    public decimal Total => ((Quantity * Price) - Discount);
    [ObservableProperty]
    DateTime _orderDate = DateTime.Today;
    [ObservableProperty] int _invoceType; // نوع الفاتورة {كاش,اجل}و
    public decimal TotalPrice => OrderDetails.Sum(x => x.Total);

    [ObservableProperty]
    //[RegularExpression(@" ^ [0 - 9] * $")]
    [NotifyPropertyChangedFor(nameof(NetPrice))]
    decimal _totalDiscount;
    public decimal NetPrice => TotalPrice - TotalDiscount;

    [ObservableProperty]
    //[RegularExpression(@" ^ [0 - 9] * $")]
    int _suppInvID;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Remaining))]
    decimal _payed;

    public decimal Remaining => NetPrice - Payed;

    [ObservableProperty] SupplierModel _supplier = new();
    [ObservableProperty] ObservableCollection<SupplierModel> _suppliers = [];
    [ObservableProperty] EmployeeModel _employee = new();
    [ObservableProperty] ObservableCollection<EmployeeModel> _employees = [];
    [ObservableProperty] ItemModel _item = new();
    [ObservableProperty] ObservableCollection<ItemModel> _items = [];
    [ObservableProperty] OrderDetailsModel order = new();
    [ObservableProperty]
    ObservableCollection<OrderDetailsModel> _orderDetails = [];


    [RelayCommand]
    public void AddOrderDetails()
    {
        OrderDetailsModel? item = OrderDetails.FirstOrDefault(x => x.ItemId == Item.ItemId);
        if(item == null)
        {
            Order.ItemId = Item.ItemId;
            Order.ItemName = Item.ItemName;
            Order.Quantity = Quantity;
            Order.Price = Price;
            Order.Discount = Discount;
            Order.Total = Total;
            OrderDetails.Add(Order);
            OnPropertyChanged(nameof(TotalPrice));
            OnPropertyChanged(nameof(NetPrice));
            OnPropertyChanged(nameof(Remaining));
        }
        else
            Validate("لايمكن اضافة الصنف مرتين", "Error", InfoBarSeverity.Error);
    }

    [RelayCommand]
    public void RemoveOrderDetail(OrderDetailsModel detail)
    {
        OrderDetails.Remove(detail);
        OnPropertyChanged(nameof(TotalPrice));
        OnPropertyChanged(nameof(NetPrice));
        OnPropertyChanged(nameof(Remaining));

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
        if(HasErrors)
        {
            Message = string.Join(Environment.NewLine, GetErrors().Select(e => e.ErrorMessage));
            Validate(Message, "Error", InfoBarSeverity.Error);
            return;
        }
        if(OrderDetails.Count <= 0)
            Validate("يجب اضافة اصناف اولا", "Error", InfoBarSeverity.Error);

        PurchaseModel? purchase = new()
        {
            SuppInvID = SuppInvID,
            OrderDate = OrderDate,
            TotalPrice = TotalPrice,
            TotalDiscount = TotalDiscount,
            TotalPayed = Payed,
            TotalRemaining = Remaining,
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
