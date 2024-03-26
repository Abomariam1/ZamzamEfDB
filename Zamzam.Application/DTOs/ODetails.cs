using Zamzam.Domain;

namespace Zamzam.Application.DTOs;

public class ODetails
{
    public int OrderId { get; set; }
    public int ItemId { get; set; }
    public string? ItemName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdateddBy { get; set; }

    public static implicit operator ODetails(OrderDetail v) => new()
    {
        OrderId = v.OrderId,
        ItemId = v.ItemId,
        ItemName = v.Item?.Name ?? "",
        Quantity = v.Quantity,
        Price = v.Price,
        Discount = v.Discount,
        Total = v.TotalPrice,
        CreatedBy = v.CreatedBy,
        UpdateddBy = v.UpdatedBy,
    };
}


