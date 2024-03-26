﻿using ZamzamUiCompact.Services.RepositoryServices.Inteface;

namespace ZamzamUiCompact.ViewModels.Pages;

public partial class ReturnPurchasesViewModel(IUnitOfWork unitOfWork): BaseValidator
{
    const string RePurchase = "ReturnPurchase";
    const string purchaseController = "Purchase";
    const string purchasesList = "GetSuppInvIdList";
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
    DateTime _orderDate = DateTime.Today;
    [ObservableProperty]
    decimal _totalPrice;

    [ObservableProperty] decimal _totalDiscount;
    [ObservableProperty] decimal _netPrice;
    [ObservableProperty] decimal _totalPayed;
    [ObservableProperty] decimal _totalremind;
    [ObservableProperty] int _invoceType; // نوع الفاتورة {كاش,اجل}و

    [ObservableProperty] SupplierModel _supplier = new();
    [ObservableProperty] ObservableCollection<SupplierModel> _suppliers = [];

    [ObservableProperty] EmployeeModel _employee = new();
    [ObservableProperty] ObservableCollection<EmployeeModel> _employees = [];

    [ObservableProperty] ItemModel _item = new();
    [ObservableProperty] ObservableCollection<ItemModel> _items = [];

    [ObservableProperty] OrderDetailsModel orderDetail = new();
    [ObservableProperty] ObservableCollection<OrderDetailsModel> _orderDetails = [];

    [ObservableProperty] PurchaseModel _purchase = new();

    [ObservableProperty] PurchasesInvSuppModel _purchasesInv = new();
    [ObservableProperty] ObservableCollection<PurchasesInvSuppModel> _purchasesList = [];

    #region Interface Methods
    [RelayCommand]
    public async Task GetAll()
    {
        List<SupplierModel>? suppliers = await GetSuppliers();
        Suppliers = new ObservableCollection<SupplierModel>(suppliers);
        if(suppliers.Count == 0)
            Validate("لايوجد موردين", "Error", InfoBarSeverity.Error);

        List<EmployeeModel> employees = await GetEmployees();
        Employees = new ObservableCollection<EmployeeModel>(employees);
        if(employees.Count == 0)
            Validate("لايوجد موظفين", "Error", InfoBarSeverity.Error);

        //List<ItemModel> items = await GetItems();
        //Items = new ObservableCollection<ItemModel>(items);

        List<PurchasesInvSuppModel> invSuppModels = await GetPurchasesList();
        PurchasesList = new ObservableCollection<PurchasesInvSuppModel>(invSuppModels);
        if(invSuppModels.Count == 0)
            Validate("لايوجد في الفاتورة", "Error", InfoBarSeverity.Error);

    }

    [RelayCommand]
    public async Task FillComponents()
    {
        Purchase = await GetPurchase(PurchasesInv.PurchaseId);
        ItemModel item = new();
        Items.Clear();
        Purchase.Details!.ForEach(x =>
        {
            item.ItemId = x.ItemId;
            item.ItemName = x.ItemName!;
            Items.Add(item);
        }
        );
    }

    [RelayCommand]
    public void AddOrderDetails()
    {
        int qt = Purchase.Details.Single(x => x.ItemId == Item.ItemId).Quantity;
        decimal prc = Purchase.Details.Single(x => x.ItemId == Item.ItemId).Price;
        if(Quantity > qt)
        {
            Validate($" الكمية المرتجعة اكبر من الكمية في الفاتورة, اقصي كمية هي {qt}", "Error", InfoBarSeverity.Error);
            return;
        }
        if(Price > prc)
        {
            Validate($" سعر المرتجع اكبر من سعر الفاتورة, اقصي سعر هو {prc}", "Error", InfoBarSeverity.Error);
            return;

        }
        OrderDetailsModel? item = OrderDetails.FirstOrDefault(x => x.ItemId == Item.ItemId);
        if(item == null)
        {
            OrderDetail.ItemId = Item.ItemId;
            OrderDetail.ItemName = Item.ItemName;
            OrderDetail.Quantity = Quantity;
            OrderDetail.Price = Price;
            OrderDetail.Discount = Discount;
            OrderDetail.Total = Total;
            OrderDetails.Add(OrderDetail);
            OnPropertyChanged(nameof(TotalPrice));
            OnPropertyChanged(nameof(NetPrice));
            OnPropertyChanged(nameof(Totalremind));
        }
        else
            Validate("لايمكن اضافة الصنف مرتين", "Error", InfoBarSeverity.Error);
    }

    [RelayCommand]
    public void RemoveRow(OrderDetailsModel detail)
    {
        OrderDetails.Remove(detail);
        OnPropertyChanged(nameof(TotalPrice));
        OnPropertyChanged(nameof(NetPrice));
        OnPropertyChanged(nameof(Totalremind));
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
        ReturnPurchaseModel returnPurchase = new();
        //? purchase = new()
        //{
        //    SuppInvID = SuppInvID,
        //    OrderDate = OrderDate,
        //    TotalPrice = TotalPrice,
        //    TotalDiscount = TotalDiscount,
        //    TotalPayed = Payed,
        //    TotalRemaining = Remaining,
        //    Details = [.. OrderDetails],
        //    EmployeeId = Employee.EmployeeId,
        //    InvoiceType = InvoceType,
        //    SupplierId = Supplier.SupplierId,
        //};
        Result<ReturnPurchaseModel>? result = await unitOfWork.Service<ReturnPurchaseModel>().AddAsync(purchaseController, returnPurchase);
        if(result.Succeeded)
        {

            Validate(result.Message, "Success", InfoBarSeverity.Success);
            return;
        }
        Validate(result.Message, "Error", InfoBarSeverity.Error);

        //Result<List<PurchaseModel>>? supps = await unitOfWork.Service<PurchaseModel>().GetAllAsync(purchaseController);


    }

    #endregion


    #region Private Methods
    private async Task<List<SupplierModel>> GetSuppliers()
    {
        Result<List<SupplierModel>>? result = await unitOfWork.Service<SupplierModel>()
            .GetAllAsync(supplierController);
        if(!result.Succeeded)
            return [];
        return result.Data;
    }

    private async Task<List<EmployeeModel>> GetEmployees()
    {
        Result<List<EmployeeModel>>? result = await unitOfWork.Service<EmployeeModel>()
            .GetAllAsync($"{employeeController}/getall");
        if(!result.Succeeded)
            return [];
        return result.Data;
    }

    private async Task<List<ItemModel>> GetItems()
    {
        Result<List<ItemModel>>? result = await unitOfWork.Service<ItemModel>()
            .GetAllAsync(ItemController);
        if(!result.Succeeded)
            return [];
        return result.Data;
    }

    private async Task<List<PurchasesInvSuppModel>> GetPurchasesList()
    {
        Result<List<PurchasesInvSuppModel>>? result = await unitOfWork.Service<PurchasesInvSuppModel>()
            .GetAllAsync($"{purchaseController}/{purchasesList}");
        if(!result.Succeeded)
            return [];
        return result.Data;
    }

    private async Task<PurchaseModel> GetPurchase(int purchaseNum)
    {
        Result<PurchaseModel> result = await unitOfWork.Service<PurchaseModel>()
            .GetByIdAsync($"{purchaseController}/{purchaseNum}");
        if(!result.Succeeded)
            return new PurchaseModel();
        return result.Data;
    }
    #endregion
}
