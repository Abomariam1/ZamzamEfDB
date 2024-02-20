namespace ZamzamUiCompact.Models;

public class SupplierModel: Model
{
    public int SupplierId { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

}
