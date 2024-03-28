namespace Zamzam.Application.DTOs;
public class SaleResponse
{
    public int CustomerId { get; set; } = 0;
    public string? CustomerName { get; set; } = "عميل افتراضي";
    public string? Phone { get; set; } = string.Empty;
    public string? Address { get; set; } = string.Empty;
    public long NationalCardId { get; set; } = 0;
    public string? Notes { get; set; } = string.Empty;
    public int AreaId { get; set; } = 0;
    public DateTime OrderDate { get; set; } = DateTime.Now.Date;
    public decimal TotalPrice { get; set; }
    public decimal TotalDiscount { get; set; }
    public decimal TotalPayed { get; set; } // اجمالي المدفوع
    public decimal TotalRemaining { get; set; } // اجمالي المتبقي
    public int InvoiceType { get; set; }
    public int EmployeeId { get; set; }
    public string? CreatedBy { get; set; }
    public ICollection<ODetails>? OrderDetails { get; set; }

}
