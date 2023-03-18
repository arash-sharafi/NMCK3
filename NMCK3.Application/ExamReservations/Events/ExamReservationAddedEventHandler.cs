using MediatR;
using NMCK3.Application.Common.Services;
using NMCK3.Domain.DomainEvents;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NMCK3.Application.ExamReservations.Events
{
    internal sealed class ExamReservationAddedEventHandler :
        INotificationHandler<ExamReservationAddedDomainEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public ExamReservationAddedEventHandler(IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        public async Task Handle(ExamReservationAddedDomainEvent notification, CancellationToken cancellationToken)
        {
            var exam = await _unitOfWork.Exams.GetExamById(notification.ExamId, cancellationToken);

            if (exam is null)
                return;

            var examReservation = exam.ExamReservations.FirstOrDefault(x => x.Id == notification.ExamReservationId);

            if (examReservation is null)
                return;

            await _emailService.SendExamReservationAddedEmailAsync(examReservation, cancellationToken);
        }
    }
}
