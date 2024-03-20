using System.ComponentModel.DataAnnotations;
using ZamzamUiCompact.Services.RepositoryServices.Inteface;

namespace ZamzamUiCompact.ViewModels.Pages;

public partial class PurchaseViewModel(IUnitOfWork unitOfWork): BaseValidator
{
    const string purchaseController = "Purchase";
    const string supplierController = "Supplier";
    const string employeeController = "Employee";
    const string ItemController = "Item";

    [ObservableProperty]
    DateTime _orderDate = DateTime.Today;

    [ObservableProperty]
    [RegularExpression(@" ^ [0 - 9] * $")]
    decimal _totalDiscount;

    [ObservableProperty]
    [RegularExpression(@" ^ [0 - 9] * $")]
    public int _suppInvID;


    [ObservableProperty]
    [RegularExpression(@" ^ [0 - 9] * $")]
    [NotifyPropertyChangedFor(nameof(Total))]
    int _quantity;

    [ObservableProperty]
    [RegularExpression(@" ^ [0 - 9] * $")]
    [NotifyPropertyChangedFor(nameof(Total))]
    decimal _price;

    [ObservableProperty]
    [RegularExpression(@" ^ [0 - 9] * $")]
    [NotifyPropertyChangedFor(nameof(Total))]
    decimal _discount;
    public decimal TotalPrice => OrderDetails.Sum(x => x.Total);
    [ObservableProperty] int _invoceType; // نوع الفاتورة {كاش,اجل}و
    [ObservableProperty] SupplierModel _supplier = new();
    [ObservableProperty] ObservableCollection<SupplierModel> _suppliers = [];
    [ObservableProperty] EmployeeModel _employee = new();
    [ObservableProperty] ObservableCollection<EmployeeModel> _employees = [];
    [ObservableProperty] ItemModel _item = new();
    [ObservableProperty] ObservableCollection<ItemModel> _items = [];
    [ObservableProperty] OrderDetailsModel order = new();
    [ObservableProperty] ObservableCollection<OrderDetailsModel> _orderDetails = [];

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
    public void RemoveOrderDetail(int row)
    {
        if(row >= 0)
        {

        }
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

        PurchaseModel? purchase = new()
        {
            SuppInvID = SuppInvID,
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
