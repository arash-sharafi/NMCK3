using System;
using NMCK3.Application.Abstractions.Messaging;

namespace NMCK3.Application.ExamReservations.Commands.Create
{
    public sealed record CreateExamReservationCommand(
        string ParticipantId,
        Guid ExamId,
        Guid VoucherId) : ICommand;
}
