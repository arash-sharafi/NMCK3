using NMCK3.Domain.Entities;
using NMCK3.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace NMCK3.Application.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserById(string userId, CancellationToken cancellationToken = default);
        Task<User> GetUserByEmail(Email email, CancellationToken cancellationToken = default);
    }
}