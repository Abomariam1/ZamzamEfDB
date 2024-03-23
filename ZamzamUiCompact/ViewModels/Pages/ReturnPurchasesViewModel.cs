using ZamzamUiCompact.Services.RepositoryServices.Inteface;

namespace ZamzamUiCompact.ViewModels.Pages;

public partial class ReturnPurchasesViewModel(IUnitOfWork unitOfWork): BaseValidator
{
    const string RePurchase = "";
    const string purchaseController = "Purchase";
    const string supplierController = "Supplier";
    const string employeeController = "Employee";
    const string ItemController = "Item";

    /* OrderDetails*/
    [ObservableProperty]
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
    /*----------------------------------------------------------*/

    [ObservableProperty]
    int _purchaseId;

    [ObservableProperty]
    int _suppInvID;

    [ObservableProperty]
    DateTime _orderDate;
    [ObservableProperty]
    decimal _totalPrice;

    [ObservableProperty] decimal _totalDiscount;
    [ObservableProperty] decimal _totalPayed;
    [ObservableProperty] decimal _totalremind;
    [ObservableProperty] int _invoceType; // نوع الفاتورة {كاش,اجل}و

    [ObservableProperty] SupplierModel _supplier = new();
    [ObservableProperty] ObservableCollection<SupplierModel> _suppliers = [];
    [ObservableProperty] EmployeeModel _employee = new();
    [ObservableProperty] ObservableCollection<EmployeeModel> _employees = [];
    [ObservableProperty] ItemModel _item = new();
    [ObservableProperty] ObservableCollection<ItemModel> _items = [];
    [ObservableProperty] OrderDetailsModel order = new();
    [ObservableProperty]
    static ObservableCollection<OrderDetailsModel> _orderDetails = [];

}
