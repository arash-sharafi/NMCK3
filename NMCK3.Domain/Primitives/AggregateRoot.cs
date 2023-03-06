using System;

namespace NMCK3.Domain.Primitives
{
    public abstract class AggregateRoot : Entity
    {
        protected AggregateRoot(Guid id) 
            : base(id)
        {
        }

        protected AggregateRoot()
        {

        }
    }
}