using Zamzam.Domain;
using Zamzam.Domain.Types;

namespace Zamzam.Application.DTOs;
public class PurchaseDto
{
    public int PurchaseId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal TotalDiscount { get; set; }
    public InvoiceType InvoiceType { get; set; }
    public int EmployeeId { get; set; }
    public int SupplierId { get; set; }
    public required List<ODetails> Details { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }


    public static implicit operator PurchaseDto(PurchaseOrder v) => new()
    {
        PurchaseId = v.Id,
        OrderDate = v.OrderDate,
        TotalPrice = v.TotalPrice,
        TotalDiscount = v.TotalDiscount,
        InvoiceType = v.InvoiceType,
        EmployeeId = v.EmployeeId,
        SupplierId = v.SupplierId,
        Details = (List<ODetails>)[.. v.OrderDetails] ?? [],
        CreatedBy = v.CreatedBy,
        UpdatedBy = v.UpdatedBy,
        CreatedOn = v.CreatedDate,
        UpdatedOn = v.UpdatedDate,
    };
}
