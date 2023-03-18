using NMCK3.Application.Common.Services;
using NMCK3.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace NMCK3.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendExamReservationAddedEmailAsync(ExamReservation examReservation, CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
        }
    }
}
