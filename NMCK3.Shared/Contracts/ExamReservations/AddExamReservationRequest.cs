using System;

namespace NMCK3.Shared.Contracts.ExamReservations
{
    public record AddExamReservationRequest(
        Guid ExamId,
        Guid VoucherId);
}
