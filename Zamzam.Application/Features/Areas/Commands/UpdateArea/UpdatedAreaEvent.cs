using Zamzam.Domain;
using Zamzam.Domain.Common;

namespace Zamzam.Application.Features.Areas.Commands.UpdateArea
{
    public class UpdatedAreaEvent : BaseEvent
    {
        public Area Area { get; }
        public UpdatedAreaEvent(Area area)
        {
            Area = area;
        }
    }
}
