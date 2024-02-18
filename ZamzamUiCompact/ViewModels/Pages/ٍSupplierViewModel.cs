using System.ComponentModel.DataAnnotations;
using ZamzamUiCompact.Services.RepositoryServices.Inteface;

namespace ZamzamUiCompact.ViewModels.Pages;

public partial class SupplierViewModel : ObservableValidator
{
    const string supplierController = "Supplier";
    private IUnitOfWork _unitOfWork;

    #region Public Properities

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
    SupplierModel _supplier;

    [ObservableProperty]
    ObservableCollection<SupplierViewModel> _suppliers;

    [ObservableProperty] InfoBarService _infoBarService;
    #endregion

    public SupplierViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #region Interface Method
    [RelayCommand]
    public async Task<Result<SupplierModel>> AddSupplierAsync()
    {
        if (SupplierName == string.Empty)
        {
            var sup = new SupplierModel()
            {
                SupplierName = SupplierName,
                Phone = Phone,
                Address = Address,
            };
            var result = await _unitOfWork.Service<SupplierModel>().AddAsync(supplierController, Supplier);
        }
        return await Result<SupplierModel>.SuccessAsync(Supplier);
    }
    [RelayCommand]
    public async Task<Result<SupplierModel>> UpdateSupplierAsync()
    {

        return await Result<SupplierModel>.SuccessAsync(Supplier);
    }
    [RelayCommand]
    public async Task DeleteSupplierAsync()
    {
        await Task.Delay(300);
    }

    #endregion
}
