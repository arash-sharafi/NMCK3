using FluentValidation;
using System;

namespace NMCK3.Application.ExamReservations.Commands.Add
{
    internal class AddExamReservationCommandValidator : AbstractValidator<AddExamReservationCommand>
    {
        public AddExamReservationCommandValidator()
        {
            RuleFor(x => x.ParticipantId).NotNull().NotEmpty();
            RuleFor(x => x.ExamId).NotEqual(Guid.Empty);
            RuleFor(x => x.VoucherCode).NotNull().NotEmpty();
        }
    }
}