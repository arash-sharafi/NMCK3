using NMCK3.Domain.Primitives;
using System;

namespace NMCK3.Domain.DomainEvents
{
    public sealed record ExamReservationAddedDomainEvent(Guid ExamId):IDomainEvent
    {
        
    }
}
