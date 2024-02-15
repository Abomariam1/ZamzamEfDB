namespace ZamzamUiCompact.Models;

public class ItemModel : Model
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
    public override int GetHashCode()
    {
        int hashCode = 66;
        hashCode = (hashCode * 19) + ItemId.GetHashCode();
        return hashCode;
    }
}
