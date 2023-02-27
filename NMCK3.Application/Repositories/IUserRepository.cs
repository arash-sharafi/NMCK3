using NMCK3.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NMCK3.Application.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserById(Guid userId, CancellationToken cancellationToken = default);
    }
}