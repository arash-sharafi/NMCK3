using NMCK3.Application.Repositories;
using NMCK3.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NMCK3.Infrastructure.Persistance.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new();
        public async Task<User> GetUserById(Guid userId, CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
            return _users.FirstOrDefault(x => x.Id == userId);
        }
    }
}