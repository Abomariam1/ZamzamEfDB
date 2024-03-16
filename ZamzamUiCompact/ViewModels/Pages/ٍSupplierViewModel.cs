using System.ComponentModel.DataAnnotations;
using ZamzamUiCompact.Services.RepositoryServices.Inteface;

namespace ZamzamUiCompact.ViewModels.Pages;

public partial class SupplierViewModel(IUnitOfWork unitOfWork): ObservableValidator
{
    const string supplierController = "Supplier";
    private readonly DispatcherTimer _dispatcher = new()
    {
        Interval = TimeSpan.FromMilliseconds(100)
    };
    private static int counter = 0;

    #region Public Properities
    [ObservableProperty] int _supplierId;
    [ObservableProperty]
    [Required]
    [MaxLength(100)]
    [MinLength(3)]
    string _supplierName = string.Empty;

    [ObservableProperty]
    string _phone = string.Empty;

    [ObservableProperty]
    string _address = string.Empty;

    [ObservableProperty]
    SupplierModel _supplier = new();

    [ObservableProperty]
    ObservableCollection<SupplierModel> _suppliers = new();
    [ObservableProperty] string _message = string.Empty;
    [ObservableProperty] bool _status = false;
    [ObservableProperty] InfoBarSeverity _saverty = InfoBarSeverity.Informational;
    [ObservableProperty] string _infoBarTitle = "رسالة";

    //[ObservableProperty] InfoBarService _infoBarService = new();

    #endregion

    #region Interface Method
    [RelayCommand]
    public async Task AddSupplierAsync()
    {
        if(SupplierName != string.Empty)
        {
            var sup = new SupplierModel()
            {
                SupplierName = SupplierName,
                Phone = Phone,
                Address = Address,
            };
            Result<SupplierModel>? result = await unitOfWork.Service<SupplierModel>().AddAsync(supplierController, sup);
            SupplierModel? supp = result.Data;
            Suppliers.Add(supp);
            Validate(result.Message, "Success", InfoBarSeverity.Success);
            return;
        }
        Validate("يجب ادخال اسم المورد", "Success", InfoBarSeverity.Success);
    }
    [RelayCommand]
    public async Task UpdateSupplierAsync()
    {
        if(Supplier != null && Supplier?.SupplierId != 0)
        {
            SupplierModel sup = new()
            {
                SupplierId = Supplier!.SupplierId,
                SupplierName = SupplierName,
                Phone = Phone,
                Address = Address,
            };
            Result<SupplierModel>? result = await unitOfWork.Service<SupplierModel>().UpdateAsync(supplierController, sup);
            SupplierModel? supp = result.Data;
            await GetAllSuppiers();
            Validate(result.Message, "Success", InfoBarSeverity.Success);

        }
    }
    [RelayCommand]
    public async Task DeleteSupplierAsync()
    {
        if(Supplier != null || Supplier?.SupplierId != 0)
        {
            Result<int>? result = await unitOfWork.Service<SupplierModel>().DeleteAsync($"{supplierController}/{Supplier!.SupplierId}");
            if(result.Succeeded)
            {
                await GetAllSuppiers();
                Validate(result.Message, "Success", InfoBarSeverity.Success);

            }
            else
            {
                Validate(result.Message, "Success", InfoBarSeverity.Success);
            }
        }
    }

    [RelayCommand]
    public async Task GetAllSuppiers()
    {
        Result<List<SupplierModel>>? result = await unitOfWork.Service<SupplierModel>().GetAllAsync(supplierController);
        Suppliers = new ObservableCollection<SupplierModel>(result.Data);
    }

    [RelayCommand]
    public void FillTexts()
    {
        if(Supplier != null)
        {
            SupplierId = Supplier.SupplierId;
            SupplierName = Supplier.SupplierName;
            Phone = Supplier.Phone;
            Address = Supplier.Address;
        }
    }
    #endregion
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
