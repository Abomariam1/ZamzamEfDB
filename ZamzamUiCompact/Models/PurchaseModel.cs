namespace ZamzamUiCompact.Models;

public class PurchaseModel: Model
{
    public int SuppInvID { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal TotalDiscount { get; set; }
    public int InvoiceType { get; set; }
    public int EmployeeId { get; set; }
    public int SupplierId { get; set; }
    public required List<OrderDetailsModel> Details { get; set; }
    public string? CreatedBy { get; set; }

}
