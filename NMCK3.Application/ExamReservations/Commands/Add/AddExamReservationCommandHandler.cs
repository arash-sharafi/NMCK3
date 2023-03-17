using NMCK3.Application.Abstractions.Messaging;
using NMCK3.Application.Common;
using NMCK3.Application.Common.Errors;
using NMCK3.Application.Common.Services;
using NMCK3.Application.Repositories;
using NMCK3.Domain.Entities;
using NMCK3.Domain.Shared;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NMCK3.Domain.ValueObjects;

namespace NMCK3.Application.ExamReservations.Commands.Add
{
    internal sealed class AddExamReservationCommandHandler : ICommandHandler<AddExamReservationCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;


        public AddExamReservationCommandHandler(IExamRepository examRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider)
        {
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Result> Handle(AddExamReservationCommand request, CancellationToken cancellationToken)
        {
            var voucherCode = VoucherCode.Create(request.VoucherCode);

            if (voucherCode.IsFailure)
                return Result.Fail(voucherCode.Error);

            var voucher = await _unitOfWork.Vouchers.GetVoucherByCode(voucherCode.Value, cancellationToken);
            var exam = await _unitOfWork.Exams.GetExamById(request.ExamId, cancellationToken);
            var participant = await _unitOfWork.Users.GetUserById(request.ParticipantId, cancellationToken);


            if (!IsVoucherValid(voucher, exam, participant, _dateTimeProvider.Now))
                return Result.Fail(ApplicationErrors.Voucher.InvalidReservationAttempt);

            exam.AddExamReservation(participant, voucher);

            await _unitOfWork.CompleteAsync(cancellationToken);

            return Result.Success();
        }

        public static bool IsVoucherValid(Voucher voucher, Exam exam, User participant, DateTime currentDate)
        {
            if (currentDate > Utilities.GetVoucherExpirationDate(voucher, Utilities.VoucherValidationInMonths))
                return false;

            var voucherAlreadyUsed = exam.ExamReservations
                .FirstOrDefault(x => x.Participant.Id == participant.Id && x.Voucher.Id == voucher.Id);

            if (voucherAlreadyUsed is not null)
                return false;

            var participantAlreadyHasAReserevation = exam.ExamReservations
                .FirstOrDefault(x => x.Participant.Id == participant.Id);

            if (participantAlreadyHasAReserevation is not null)
            {
                return false;
            }

            return true;
        }


    }
}