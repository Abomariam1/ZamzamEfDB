using Zamzam.Domain;
using Zamzam.Domain.Common;

namespace Zamzam.Application.Features.Items.Commands.Create
{
    public class ItemCreatedEvet : BaseEvent
    {
        public Item Item { get; }
        public ItemCreatedEvet(Item item)
        {
            Item = item;
        }
    }
}
