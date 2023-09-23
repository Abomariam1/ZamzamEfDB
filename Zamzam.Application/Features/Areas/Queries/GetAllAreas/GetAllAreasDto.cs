using Zamzam.Application.Common.Mappings;
using Zamzam.Domain;

namespace Zamzam.Application.Features.Areas.Queries.GetAllAreas
{
    public class GetAllAreasDto : IMapFrom<Area>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Station { get; set; }
        public string? Location { get; set; }
        public string CollectorName { get; set; }

    }
}
