﻿using System.ComponentModel.DataAnnotations.Schema;
using Zamzam.Core.Entites.Interfaces;

namespace Zamzam.Core
{

    public abstract class BaseEntity : IEntity
    {
        private readonly List<BaseEvent> _events = new();
        public int Id { get; set; }

        [NotMapped]
        public IReadOnlyCollection<BaseEvent> Events => _events.AsReadOnly();

        public void AddDomainEvent(BaseEvent domainEvent) => _events.Add(domainEvent);
    }
}
