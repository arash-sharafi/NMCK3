using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NMCK3.Application;
using NMCK3.Application.Abstractions.Authentication;
using NMCK3.Application.Common.Services;
using NMCK3.Application.Repositories;
using NMCK3.Infrastructure.Authentication;
using NMCK3.Infrastructure.Persistance;
using NMCK3.Infrastructure.Persistance.Models;
using NMCK3.Infrastructure.Persistance.Repositories;
using NMCK3.Infrastructure.Services;

namespace NMCK3.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            services.AddScoped<IExamRepository, IExamRepository>();
            services.AddScoped<IVoucherRepository, VoucherRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            AddAuth(services);

            return services;
        }

        private static IServiceCollection AddAuth(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();

            services.AddAuthorization();

            services.AddSingleton<IJwtTokenProvider, JwtTokenProvider>();
            services.AddSingleton<IRegisterService, RegisterService>();

            return services;
        }
    }
}
