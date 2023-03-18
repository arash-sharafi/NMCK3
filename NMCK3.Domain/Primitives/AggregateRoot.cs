using System;
using System.Collections.Generic;

namespace NMCK3.Domain.Primitives
{
    public abstract class AggregateRoot : Entity
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        protected AggregateRoot(Guid id)
            : base(id)
        {
        }

        protected void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        protected AggregateRoot()
        {

        }
    }
}