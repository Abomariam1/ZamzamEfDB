using Zamzam.Domain;
using Zamzam.Domain.Common;

namespace Zamzam.Application.Features.Areas.Commands.DeleteArea;

public class AreaDeletedEvent(Area area) : BaseEvent
{
    public Area Area { get; } = area;
}
