using NMCK3.Application.Abstractions.Messaging;
using System;

namespace NMCK3.Application.ExamReservations.Create
{
    public sealed record CreateExamReservationCommand(
        Guid ParticipantId,
        Guid ExamId,
        Guid VoucherId) : ICommand;
}
