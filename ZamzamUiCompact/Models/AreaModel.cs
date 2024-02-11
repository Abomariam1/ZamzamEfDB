namespace ZamzamUiCompact.Models;

public record AreaModel
{
    public int AreaId { get; set; }
    public string AreaName { get; set; }
    public string Station { get; set; }
    public string? Location { get; set; }
    public int CollectorId { get; set; }
    public string CollectorName { get; set; }

    public override int GetHashCode()
    {
        int hashCode = 13;
        hashCode = (hashCode * 19) + AreaId.GetHashCode();
        return hashCode;
    }
}
