using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NMCK3.Application.Abstractions.Authentication;
using NMCK3.Infrastructure.Persistance.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NMCK3.Infrastructure.Authentication
{
    internal sealed class JwtTokenProvider : IJwtTokenProvider
    {
        private readonly JwtOptions _options;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public JwtTokenProvider(IOptions<JwtOptions> options,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _options = options.Value;
        }

        public async Task<string> Generate(
            string email, 
            string password, 
            CancellationToken cancellationToken = default)
        {
            if (!await IsUserValid(email, password)) return null;

            var user = await _userManager.FindByEmailAsync(email);

            if (user is null) return null;

            var role = (await _userManager.GetRolesAsync(user))[0];
            var jwtToken = GenerateToken(user, role);

            await _userManager.UpdateAsync(user);

            return jwtToken;

        }

        private string GenerateToken(ApplicationUser user, string role)
        {
            var claims = new Claim[]
            {
                new(JwtRegisteredClaimNames.Sub,user.Id),
                new(JwtRegisteredClaimNames.Email,user.Email),
                new(ClaimTypes.Role,role)
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _options.Issuer,
                _options.Audience,
                claims,
                null,
                DateTime.Now.AddHours(1),
                signingCredentials);

            string tokenValue = new JwtSecurityTokenHandler()
                .WriteToken(token);

            return tokenValue;
        }

        private async Task<bool> IsUserValid(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                return false;
            }

            var singInResult = await _signInManager.PasswordSignInAsync(email, password, true, false);

            return singInResult.Succeeded;
        }
    }


}
