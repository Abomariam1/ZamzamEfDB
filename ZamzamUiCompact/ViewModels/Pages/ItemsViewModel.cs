using System.ComponentModel.DataAnnotations;
using ZamzamUiCompact.Services.RepositoryServices.Inteface;

namespace ZamzamUiCompact.ViewModels.Pages;

public partial class ItemsViewModel(IUnitOfWork unitOfWork): ObservableValidator
{
    const string ItemController = "Item";
    private readonly DispatcherTimer _dispatcher = new()
    {
        Interval = TimeSpan.FromMilliseconds(100)
    };
    private static int counter = 0;

    [Required][ObservableProperty] int _itemId;
    [Required]
    [MaxLength(50)]
    [ObservableProperty]
    string _itemName = string.Empty;
    [Required][ObservableProperty] decimal _purchasingPrice;
    [Required][ObservableProperty] decimal _sellingCashPrice;
    [Required][ObservableProperty] decimal _installmentPrice;
    [Required][ObservableProperty] int _balance;
    [ObservableProperty] string? _createdBy;
    [ObservableProperty] DateTime _createdOn;
    [ObservableProperty] string? _updatedBy;
    [ObservableProperty] DateTime _updatedOn;
    [ObservableProperty] ItemModel _item = new();
    [ObservableProperty] ObservableCollection<ItemModel> _items = [];
    [ObservableProperty] string _message = string.Empty;
    [ObservableProperty] bool _status = false;
    [ObservableProperty] InfoBarSeverity _saverty = InfoBarSeverity.Informational;
    [ObservableProperty] string _infoBarTitle = "رسالة";

    [RelayCommand]
    public async Task AddItemAsync()
    {
        ValidateAllProperties();
        if(!HasErrors)
        {
            FillItemProperty();
            Result<ItemModel>? result = await unitOfWork.Service<ItemModel>().AddAsync(ItemController, Item);
            if(result.Succeeded)
            {
                ItemModel? itm = result.Data;
                Items.Add(itm);
                Validate(result.Message, "Success", InfoBarSeverity.Success);
                return;
            }
        }
        else
        {
            Message = string.Join(Environment.NewLine, GetErrors().Select(e => e.ErrorMessage));
            Validate(Message, "Error", InfoBarSeverity.Error);
        }
    }

    [RelayCommand]
    public async Task UpdateItemAsync()
    {
        ValidateAllProperties();

        if(!HasErrors)
        {
            if(Item.ItemId > 0)
            {
                FillItemProperty();
                Result<ItemModel>? result = await unitOfWork.Service<ItemModel>().UpdateAsync(ItemController, Item);
                if(result.Succeeded)
                {
                    Message = result.Message;
                    int index = Items.IndexOf(Item);
                    List<ItemModel>? newList = Items.ToList();
                    newList.RemoveAt(index);
                    newList.Insert(index, Item);
                    Items.Clear();
                    Items = new ObservableCollection<ItemModel>(newList);
                    Validate(result.Message, "Success", InfoBarSeverity.Success);
                    return;
                }
                else
                {
                    Validate(result.Message, "Error", InfoBarSeverity.Error);
                    return;
                }
            }
            Validate("يجب اختيار صنف للتعديل", "Warning", InfoBarSeverity.Warning);
        }
        else
        {
            Message = string.Join(Environment.NewLine, GetErrors().Select(e => e.ErrorMessage));
            Validate(Message, "Error", InfoBarSeverity.Error);
        }


    }

    [RelayCommand]
    public async Task RemoveItemAsync()
    {
        ValidateAllProperties();

        if(!HasErrors)
        {
            if(Item != null)
            {
                Result<int>? result = await unitOfWork.Service<ItemModel>().DeleteAsync($"{ItemController}/{Item.ItemId}");
                if(result.Succeeded)
                {
                    Message = result.Message;
                    var index = Items.IndexOf(Items.FirstOrDefault(x => x.ItemId == Item.ItemId));
                    if(index > -1) Items.RemoveAt(index);
                    Validate(result.Message, "Success", InfoBarSeverity.Success);
                }
                else
                    Validate(result.Message, "Error", InfoBarSeverity.Error);
            }
            else
                Validate("يجب اختيار صنف للحذف", "Warning", InfoBarSeverity.Warning);

        }
        else
        {
            Message = string.Join(Environment.NewLine, GetErrors().Select(e => e.ErrorMessage));
            Validate(Message, "Error", InfoBarSeverity.Error);
        }

    }

    [RelayCommand]
    public async Task<List<ItemModel>> GetAllItemsAsync()
    {
        Result<List<ItemModel>>? items = await unitOfWork.Service<ItemModel>().GetAllAsync(ItemController);
        if(items.Succeeded)
        {
            Items = new ObservableCollection<ItemModel>(items.Data);
        }
        return [];
    }

    [RelayCommand]
    public void FillText()
    {
        if(Item == null) return;
        ItemId = Item.ItemId;
        ItemName = Item.ItemName;
        PurchasingPrice = Item.PurchasingPrice;
        SellingCashPrice = Item.SellingCashPrice;
        InstallmentPrice = Item.InstallmentPrice;
        Balance = Item.Balance;
    }

    [RelayCommand]
    public void FillItemProperty()
    {
        Item.ItemId = ItemId == 0 ? 0 : ItemId;
        Item.ItemName = ItemName;
        Item.PurchasingPrice = PurchasingPrice;
        Item.SellingCashPrice = SellingCashPrice;
        Item.InstallmentPrice = InstallmentPrice;
        Item.Balance = Balance;
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
