using NMCK3.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;
using NMCK3.Domain.ValueObjects;

namespace NMCK3.Application.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserById(Guid userId, CancellationToken cancellationToken = default);
        Task<User> GetUserByEmail(Email email, CancellationToken cancellationToken = default);
    }
}