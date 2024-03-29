﻿namespace ZamzamUiCompact.Models;

public class PurchaseModel: Model
{
    public int SuppInvID { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal TotalDiscount { get; set; }
    public decimal TotalPayed { get; set; }
    public decimal TotalRemaining { get; set; }
    public int InvoiceType { get; set; }
    public int EmployeeId { get; set; }
    public int SupplierId { get; set; }
    public List<OrderDetailsModel>? Details { get; set; }
    public string? CreatedBy { get; set; }

}
