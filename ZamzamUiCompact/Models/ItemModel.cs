namespace ZamzamUiCompact.Models;

public class ItemModel: Model
{
    public int ItemId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public decimal PurchasingPrice { get; set; }
    public decimal SellingCashPrice { get; set; }
    public decimal InstallmentPrice { get; set; }
    public int Balance { get; set; }
    public string? CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public string? UpdatedBy { get; set; } = string.Empty;
    public DateTime UpdatedOn { get; set; } = DateTime.Now;

    public override bool Equals(object? obj)
    {
        if(obj == null) return false;
        if(ReferenceEquals(this, obj)) return true;
        if(obj is not ItemModel other) return false;
        if(ReferenceEquals(this, other)) return true;

        return ItemId == other.ItemId
            && ItemName == other.ItemName;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ItemId, ItemName, PurchasingPrice, SellingCashPrice, InstallmentPrice, Balance);
    }
}
