using Zamzam.Application.Common.Mappings;
using Zamzam.Domain;

namespace Zamzam.Application.DTOs
{
    public class AreaDto : IMapFrom<Area>
    {
        public int AreaId { get; set; }
        public string AreaName { get; set; } = string.Empty;
        public string Station { get; set; } = string.Empty;
        public string? Location { get; set; } = string.Empty;
        public int CollectorId { get; set; }
        public string? CollectorName { get; set; } = string.Empty;
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; } = DateTime.Now;

        public static explicit operator AreaDto(Area v)
        {
            return new AreaDto
            {
                AreaId = v.Id,
                AreaName = v.Name,
                Station = v.Station,
                Location = v.Location,
                CollectorId = v.EmployeeId,
                CollectorName = v.Employee.Name ?? "",
                CreatedBy = v.CreatedBy,
                CreatedDate = v.CreatedDate,
                UpdatedBy = v.UpdatedBy,
                UpdatedDate = v.UpdatedDate
            };

        }
    }
}
