namespace ZamzamUiCompact.Models;
public class OrderDetailsModel: Model
{
    public int? OrderId { get; set; }
    public int ItemId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdateddBy { get; set; }
}
