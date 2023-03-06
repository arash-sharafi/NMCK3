using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NMCK3.Application.Repositories;
using NMCK3.Domain.Entities;
using NMCK3.Domain.ValueObjects;

namespace NMCK3.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new();
        public async Task<User> GetUserById(string userId, CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
            return _users.FirstOrDefault(x => x.Id == userId);
        }

        public async Task<User> GetUserByEmail(Email email, CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
            return _users.FirstOrDefault(x => x.Email == email);
        }
    }
}