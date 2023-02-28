using System;
using NMCK3.Application.Abstractions.Messaging;

namespace NMCK3.Application.ExamReservations.Commands.Create
{
    public sealed record CreateExamReservationCommand(
        Guid ParticipantId,
        Guid ExamId,
        Guid VoucherId) : ICommand;
}
