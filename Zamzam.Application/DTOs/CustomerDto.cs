using Zamzam.Domain;

namespace Zamzam.Application.DTOs
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = "عميل افتراضي";
        public string? Phone { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public long NationalCardId { get; set; } = 0;
        public string? Notes { get; set; } = string.Empty;
        public bool IsProplem { get; set; } = false;
        public bool IsBlackList { get; set; } = false;
        public int AreaId { get; set; }
        public AreaDto? Area { get; set; }

        public static explicit operator CustomerDto(Customer v) => v == null ? new CustomerDto() :
                new CustomerDto
                {
                    CustomerId = v.Id,
                    CustomerName = v.Name,
                    Phone = v.Phone,
                    Address = v.Address,
                    NationalCardId = v.NationalCardId,
                    AreaId = v.AreaId,
                    Notes = v.Notes,
                    IsProplem = v.IsProplem,
                    IsBlackList = v.IsBlackList,
                    Area = (AreaDto)v.Area ?? new AreaDto()
                };
    }
}
