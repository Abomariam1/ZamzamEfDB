using Zamzam.Domain;
using Zamzam.Domain.Common;

namespace Zamzam.Application.Features.Areas.Commands.CreateArea
{
    public class CreatedAreaEvent : BaseEvent
    {
        public Area Area { get; }
        public CreatedAreaEvent(Area area)
        {
            Area = area;
        }

    }
}
