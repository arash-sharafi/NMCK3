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
            var voucher = await _unitOfWork.Vouchers.GetVoucherById(request.VoucherId, cancellationToken);
            var exam = await _unitOfWork.Exams.GetExamById(request.ExamId, cancellationToken);
            var participant = await _unitOfWork.Users.GetUserById(request.ParticipantId, cancellationToken);


            if (!IsVoucherValid(voucher, exam, participant, _dateTimeProvider.Now))
                return Result.Fail(ApplicationErrors.Voucher.InvalidVoucher);

            //Add the reservation
            exam.SubmitReservation(participant, voucher);

            //Save the result
            await _unitOfWork.CompleteAsync(cancellationToken);

            return Result.Success();
        }

        public static bool IsVoucherValid(Voucher voucher, Exam exam, User participant, DateTime currentDate)
        {
            //Check of voucher is expired
            if (currentDate > Utilities.GetVoucherExpirationDate(voucher, Utilities.VoucherValidationInMonths))
                return false;

            //Voucher used before
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