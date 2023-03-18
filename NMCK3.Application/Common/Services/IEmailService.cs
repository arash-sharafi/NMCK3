using NMCK3.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace NMCK3.Application.Common.Services
{
    public interface IEmailService
    {
        Task SendExamReservationAddedEmailAsync(Exam exam, CancellationToken cancellationToken = default);
    }
}
