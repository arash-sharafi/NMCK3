using NMCK3.Application.Abstractions.Messaging;
using System;

namespace NMCK3.Application.Exams.Commands.Create
{
    public sealed record CreateExamCommand(
        string Name,
        string ExamDate,
        string Description,
        int Capacity) : ICommand<Guid>;
}
