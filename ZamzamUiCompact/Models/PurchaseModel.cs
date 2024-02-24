namespace ZamzamUiCompact.Models;

public class PurchaseModel : Model
{
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal TotalDiscount { get; set; }
    public string InvoiceType { get; set; } = string.Empty;
    public int EmployeeId { get; set; }
    public int SupplierId { get; set; }
    public required List<OrderDetailsModel> Details { get; set; }
    public string? CreatedBy { get; set; }

}
