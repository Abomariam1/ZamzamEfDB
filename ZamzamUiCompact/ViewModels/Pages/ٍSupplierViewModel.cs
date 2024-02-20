using System.ComponentModel.DataAnnotations;
using ZamzamUiCompact.Services.RepositoryServices.Inteface;

namespace ZamzamUiCompact.ViewModels.Pages;

public partial class SupplierViewModel: ObservableValidator
{
    const string supplierController = "Supplier";
    private IUnitOfWork _unitOfWork;

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

    [ObservableProperty] InfoBarService _infoBarService = new();
    #endregion

    public SupplierViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

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
            Result<SupplierModel>? result = await _unitOfWork.Service<SupplierModel>().AddAsync(supplierController, sup);
            SupplierModel? supp = result.Data;
            Suppliers.Add(supp);
        }
    }
    [RelayCommand]
    public async Task UpdateSupplierAsync()
    {
        if(Supplier != null || Supplier?.SupplierId != 0)
        {
            SupplierModel sup = new()
            {
                SupplierId = Supplier!.SupplierId,
                SupplierName = SupplierName,
                Phone = Phone,
                Address = Address,
            };
            Result<SupplierModel>? result = await _unitOfWork.Service<SupplierModel>().UpdateAsync(supplierController, sup);
            SupplierModel? supp = result.Data;
            await GetAllSuppiers();
        }
    }
    [RelayCommand]
    public async Task DeleteSupplierAsync()
    {
        if(Supplier != null || Supplier?.SupplierId != 0)
        {
            Result<int>? result = await _unitOfWork.Service<SupplierModel>().DeleteAsync($"{supplierController}/{Supplier!.SupplierId}");
            if(result != null)
            {
                await GetAllSuppiers();
            }
            else
            {
                throw new InvalidDataException();
            }
        }
    }

    [RelayCommand]
    public async Task GetAllSuppiers()
    {
        Result<List<SupplierModel>>? result = await _unitOfWork.Service<SupplierModel>().GetAllAsync(supplierController);
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
}
