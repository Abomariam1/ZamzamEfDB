using System.ComponentModel.DataAnnotations;
using ZamzamUiCompact.Services.RepositoryServices.Inteface;

namespace ZamzamUiCompact.ViewModels.Pages;

public partial class ItemsViewModel : ObservableValidator
{
    private readonly IUnitOfWork _unitOfWork;
    const string ItemController = "Item";

    [Required][ObservableProperty] int _itemId;
    [Required][ObservableProperty] string _itemName = string.Empty;
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
    public ItemsViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [RelayCommand]
    public async Task AddItemAsync()
    {
        if (IsReady())
        {
            FillItemProperty();
            Result<ItemModel>? addedItem = await _unitOfWork.Service<ItemModel>().AddAsync(ItemController, Item);
            if (addedItem.Succeeded)
            {
                ItemModel? itm = addedItem.Data;
                Items.Add(itm);
                Message = addedItem.Message;
                return;
            }
            Message = addedItem.Message;
            return;
        }
    }

    [RelayCommand]
    public async Task UpdateItemAsync()
    {
        if (IsReady())
        {
            if (Item.ItemId > 0)
            {
                FillItemProperty();
                Result<ItemModel>? result = await _unitOfWork.Service<ItemModel>().UpdateAsync(ItemController, Item);
                if (result.Succeeded)
                {
                    Message = result.Message;
                    int index = Items.IndexOf(Item);
                    List<ItemModel>? newList = Items.ToList();
                    newList.RemoveAt(index);
                    newList.Insert(index, Item);
                    Items.Clear();
                    Items = new ObservableCollection<ItemModel>(newList);
                    return;
                }
                Message = result.Message;
            }

        }

    }

    [RelayCommand]
    public async Task RemoveItemAsync()
    {
        if (IsReady())
        {
            if (Item.ItemId > 0)
            {
                var result = await _unitOfWork.Service<ItemModel>().DeleteAsync($"{ItemController}/{Item.ItemId}");
                if (result.Succeeded)
                {
                    Message = result.Message;
                    Items.Remove(Item);
                }
            }
        }
    }

    [RelayCommand]
    public async Task<List<ItemModel>> GetAllItemsAsync()
    {
        Result<List<ItemModel>>? items = await _unitOfWork.Service<ItemModel>().GetAllAsync(ItemController);
        if (items.Succeeded)
        {
            Items = new ObservableCollection<ItemModel>(items.Data);
        }
        return [];
    }

    private bool IsReady()
    {
        bool ready = ItemName is null || PurchasingPrice > SellingCashPrice || SellingCashPrice > InstallmentPrice;
        return !ready;
    }

    private void FillItemProperty()
    {
        Item.ItemId = ItemId == 0 ? 0 : ItemId;
        Item.ItemName = ItemName;
        Item.PurchasingPrice = PurchasingPrice;
        Item.SellingCashPrice = SellingCashPrice;
        Item.InstallmentPrice = InstallmentPrice;
        Item.Balance = Balance;
    }
}
