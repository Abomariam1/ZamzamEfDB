using Zamzam.Domain;
using Zamzam.Domain.Common;

namespace Zamzam.Application.Features.Areas.Commands.DeleteArea
{
    public class DeletedAreaEvent : BaseEvent
    {
        public Area Area { get; }
        public DeletedAreaEvent(Area area)
        {
            Area = area;
        }
    }
}
