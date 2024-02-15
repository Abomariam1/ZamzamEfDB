using Zamzam.Domain;

namespace Zamzam.Application.DTOs;

public class ItemDTO
{
    public int ItemId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public decimal PurchasingPrice { get; set; }
    public decimal SellingCashPrice { get; set; }
    public decimal InstallmentPrice { get; set; }
    public int Balance { get; set; }
    public string? CreatedBy { get; set; } = string.Empty;
    public DateTime? CreatedOn { get; set; } = DateTime.Now;
    public string? UpdatedBy { get; set; } = string.Empty;
    public DateTime? UpdatedOn { get; set; } = DateTime.Now;

    public static explicit operator ItemDTO(Item v) => new()
    {
        ItemId = v.Id,
        ItemName = v.Name,
        PurchasingPrice = v.PurchasingPrice,
        SellingCashPrice = v.SellingCashPrice,
        InstallmentPrice = v.InstallmentPrice,
        Balance = v.Balance,
        CreatedBy = v.CreatedBy,
        CreatedOn = v.CreatedDate,
        UpdatedBy = v.UpdatedBy,
        UpdatedOn = v.UpdatedDate
    };
}
