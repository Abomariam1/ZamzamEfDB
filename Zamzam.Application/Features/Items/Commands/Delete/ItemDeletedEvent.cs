using Zamzam.Domain;
using Zamzam.Domain.Common;

namespace Zamzam.Application.Features.Items.Commands.Delete
{
    public class ItemDeletedEvent : BaseEvent
    {
        public Item Item { get; }
        public ItemDeletedEvent(Item item)
        {
            Item = item;
        }
    }
}
