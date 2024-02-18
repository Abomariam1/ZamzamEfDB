using Zamzam.Domain;

namespace Zamzam.Application.DTOs;

public class SupplierDto
{
    public int SupplierId { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public string UpdatedBy { get; set; } = string.Empty;
    public DateTime UpdatedOn { get; set; }

    public static explicit operator SupplierDto(Supplier v) => new()
    {
        SupplierId = v.Id,
        SupplierName = v.Name,
        Phone = v.Phone,
        Address = v.Address,
        CreatedBy = v.CreatedBy ?? "",
        CreatedOn = v.CreatedDate ?? DateTime.Now,
        UpdatedBy = v.UpdatedBy ?? "",
        UpdatedOn = v.UpdatedDate ?? DateTime.Now,
    };
}
