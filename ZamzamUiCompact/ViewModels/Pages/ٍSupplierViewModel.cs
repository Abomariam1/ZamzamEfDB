using System.ComponentModel.DataAnnotations;
using ZamzamUiCompact.Services.RepositoryServices.Inteface;

namespace ZamzamUiCompact.ViewModels.Pages;

public partial class SupplierViewModel(IUnitOfWork unitOfWork): BaseValidator
{
    const string supplierController = "Supplier";

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
    ObservableCollection<SupplierModel> _suppliers = [];

    #endregion

    #region Interface Method
    [RelayCommand]
    public async Task AddSupplierAsync()
    {
        ValidateAllProperties();

        if(!HasErrors)
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
        Message = string.Join(Environment.NewLine, GetErrors().Select(e => e.ErrorMessage));
        Validate(Message, "Error", InfoBarSeverity.Error);
    }

    [RelayCommand]
    public async Task UpdateSupplierAsync()
    {
        ValidateAllProperties();
        if(!HasErrors)
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
                return;
            }
            Validate("يجب اختيار مورد", "Error", InfoBarSeverity.Error);
            return;
        }
        Message = string.Join(Environment.NewLine, GetErrors().Select(e => e.ErrorMessage));
        Validate(Message, "Error", InfoBarSeverity.Error);
    }

    [RelayCommand]
    public async Task DeleteSupplierAsync()
    {
        ValidateAllProperties();
        if(HasErrors)
        {
            Message = string.Join(Environment.NewLine, GetErrors().Select(e => e.ErrorMessage));
            Validate(Message, "Error", InfoBarSeverity.Error);
            return;
        }
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
                Validate(result.Message, "Error", InfoBarSeverity.Error);
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

}
