using NMCK3.Application.Abstractions.Messaging;
using System;

namespace NMCK3.Application.ExamReservations.Commands.Add
{
    public sealed record AddExamReservationCommand(
        string ParticipantId,
        Guid ExamId,
        Guid VoucherId) : ICommand;
}
