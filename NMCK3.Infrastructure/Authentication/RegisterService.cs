using Microsoft.AspNetCore.Identity;
using NMCK3.Domain.Shared;
using NMCK3.Infrastructure.Persistance.Models;
using System.Threading;
using System.Threading.Tasks;

namespace NMCK3.Infrastructure.Authentication
{
    public sealed class RegisterService : IRegisterService
    {
        private const string ParticipantRole = "Participant";
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result> Register(string firstName, string lastName, string email, string password, CancellationToken cancellationToken = default)
        {
            var user = new ApplicationUser()
            {
                UserName = email,
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };

            var result = await _userManager.CreateAsync(user, password);
            if(!result.Succeeded)
                return Result.Fail(new Error("User.Register","Error occurred while registering the user."));

            await _userManager.AddToRoleAsync(user, ParticipantRole);

            return Result.Success();
        }
    }
}