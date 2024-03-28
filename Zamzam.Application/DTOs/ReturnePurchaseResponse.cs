namespace Zamzam.Application.DTOs;
public class ReturnePurchaseResponse
{
    public int PurchaseId { get; set; }
    public int SuppInvID { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal TotalDiscount { get; set; }
    public decimal TotalPayed { get; set; } // اجمالي المدفوع
    public decimal TotalRemaining { get; set; } // اجمالي المتبقي
    public int InvoiceType { get; set; }
    public string ResonForReturn { get; set; } = string.Empty;
    public int EmployeeId { get; set; }
    public int SupplierId { get; set; }
    public List<ODetails>? Details { get; set; }
    public string? CreatedBy { get; set; }

}
