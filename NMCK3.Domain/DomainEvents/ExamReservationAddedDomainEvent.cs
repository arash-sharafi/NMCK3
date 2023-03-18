using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NMCK3.Domain.Entities;
using NMCK3.Domain.Primitives;

namespace NMCK3.Domain.DomainEvents
{
    public sealed record ExamReservationAddedDomainEvent(Exam Exam):IDomainEvent
    {
    }
}
