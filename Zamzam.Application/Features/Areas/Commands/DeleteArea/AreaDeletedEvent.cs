using Zamzam.Domain;
using Zamzam.Domain.Common;

namespace Zamzam.Application.Features.Areas.Commands.DeleteArea
{
    public class AreaDeletedEvent : BaseEvent
    {
        public Area Area { get; }
        public AreaDeletedEvent(Area area)
        {
            Area = area;
        }
    }
}
