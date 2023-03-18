using MediatR;
using NMCK3.Domain.DomainEvents;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NMCK3.Application.ExamReservations.Events
{
    internal sealed class ExamReservationAddedEventHandler:
        INotificationHandler<ExamReservationAddedDomainEvent>
    {
        public Task Handle(ExamReservationAddedDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
