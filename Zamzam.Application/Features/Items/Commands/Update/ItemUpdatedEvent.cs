using Zamzam.Domain;
using Zamzam.Domain.Common;

namespace Zamzam.Application.Features.Items.Commands.Update
{
    public class ItemUpdatedEvent : BaseEvent
    {
        public Item Item { get; }
        public ItemUpdatedEvent(Item item)
        {
            Item = item;
        }
    }
}
