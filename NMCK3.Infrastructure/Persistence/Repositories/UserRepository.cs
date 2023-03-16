using Microsoft.EntityFrameworkCore;
using NMCK3.Application.Repositories;
using NMCK3.Domain.Entities;
using NMCK3.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace NMCK3.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserById(string userId, CancellationToken cancellationToken = default)
        {
            var applicationUser = await _context.Users.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

            var user = User.Create(applicationUser.Id, applicationUser.Email);
            return user.IsFailure ? null : user.Value;
        }

        public async Task<User> GetUserByEmail(Email email, CancellationToken cancellationToken = default)
        {
            var applicationUser = await _context.Users.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email.Value, cancellationToken);

            var user = User.Create(applicationUser.Id, applicationUser.Email);
            return user.IsFailure ? null : user.Value;
        }
    }
}