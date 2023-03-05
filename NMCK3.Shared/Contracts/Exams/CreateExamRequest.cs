namespace NMCK3.Shared.Contracts.Exams
{
    public record CreateExamRequest(
        string Name,
        string ExamDate,
        string Description,
        int Capacity);
}
