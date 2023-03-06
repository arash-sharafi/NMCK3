﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NMCK3.Application;
using NMCK3.Application.Abstractions.Authentication;
using NMCK3.Application.Common.Services;
using NMCK3.Application.Repositories;
using NMCK3.Infrastructure.Authentication;
using NMCK3.Infrastructure.Persistence;
using NMCK3.Infrastructure.Persistence.Models;
using NMCK3.Infrastructure.Persistence.Repositories;
using NMCK3.Infrastructure.Services;

namespace NMCK3.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {

            services
                .AddPersistence(configuration)
                .AddAuth();

            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IExamRepository, ExamRepository>();
            services.AddScoped<IVoucherRepository, VoucherRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        private static void AddAuth(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();

            services.AddAuthorization();

            services.AddScoped<IJwtTokenProvider, JwtTokenProvider>();
            services.AddScoped<IRegisterService, RegisterService>();
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
