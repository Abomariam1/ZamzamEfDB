using Zamzam.Domain;

namespace Zamzam.Application.DTOs;
public class PurchacesSuppInvDto
{
    public int PurchaseId { get; set; }
    public int SuppInvID { get; set; } // رقم فاتورة المورد

    public static implicit operator PurchacesSuppInvDto(PurchaseOrder v) => new()
    {
        PurchaseId = v.Id,
        SuppInvID = v.InvoiceNumber,
    };

}
