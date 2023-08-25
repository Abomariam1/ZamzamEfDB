using System.ComponentModel.DataAnnotations.Schema;
using Zamzam.Domain.Common.Interfaces;

namespace Zamzam.Domain.Common
{

    public abstract class BaseEntity : IEntity
    {
        private readonly List<BaseEvent> _domainEvents = new();
        public int Id { get; set; }

        [NotMapped]
        public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

        public bool IsDeleted { get; }

        public void AddDomainEvent(BaseEvent domainEvent) => _domainEvents.Add(domainEvent);
    }
}
