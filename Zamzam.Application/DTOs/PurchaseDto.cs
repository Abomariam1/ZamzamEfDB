using Zamzam.Domain;
using Zamzam.Domain.Types;

namespace Zamzam.Application.DTOs;
public class PurchaseDto
{
    public required DateTime OrderDate { get; set; }
    public required decimal TotalPrice { get; set; }
    public required decimal TotalDiscount { get; set; }
    public required InvoiceType InvoiceType { get; set; }
    public required int EmployeeId { get; set; }
    public required int SupplierId { get; set; }
    public required List<ODetails> Details { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }


    public static implicit operator PurchaseDto(PurchaseOrder v) =>
        new()
        {
            OrderDate = v.OrderDate,
            TotalPrice = v.TotalPrice,
            TotalDiscount = v.TotalDiscount,
            InvoiceType = v.InvoiceType,
            EmployeeId = v.EmployeeId,
            SupplierId = v.SupplierId,
            Details = (List<ODetails>)v.OrderDetails,
            CreatedBy = v.CreatedBy,
            UpdatedBy = v.UpdatedBy,
            CreatedOn = v.CreatedDate,
            UpdatedOn = v.UpdatedDate,
        };
}
