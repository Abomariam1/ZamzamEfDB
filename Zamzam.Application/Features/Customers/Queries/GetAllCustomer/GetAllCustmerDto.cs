using Zamzam.Application.Common.Mappings;
using Zamzam.Domain;

namespace Zamzam.Application.Features.Customers.Queries.GetAllCustomer
{
    public class GetAllCustmerDto : IMapFrom<Customer>
    {
        public string Name { get; set; } = "عميل افتراضي";
        public string? Phone { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public long NationalCardId { get; set; } = 0;
        public string? Notes { get; set; } = string.Empty;
        public bool IsProplem { get; set; } = false;
        public bool IsBlackList { get; set; } = false;
        public int AreaId { get; set; }
    }
}
