using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NMCK3.Application;
using NMCK3.Application.Abstractions.Authentication;
using NMCK3.Application.Common.Services;
using NMCK3.Application.Repositories;
using NMCK3.Infrastructure.Authentication;
using NMCK3.Infrastructure.BackgroundJobs;
using NMCK3.Infrastructure.Persistence;
using NMCK3.Infrastructure.Persistence.Interceptors;
using NMCK3.Infrastructure.Persistence.Models;
using NMCK3.Infrastructure.Persistence.Repositories;
using NMCK3.Infrastructure.Services;
using Quartz;
using System.Text;

namespace NMCK3.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {

            services
                .AddProcessOutboxMessagesJob()
                .AddPersistence(configuration)
                .AddAuth(configuration);

            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            return services;
        }

        private static IServiceCollection AddProcessOutboxMessagesJob(this IServiceCollection services)
        {

            services.AddQuartz(configure =>
            {
                var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));

                configure.AddJob<ProcessOutboxMessagesJob>(jobKey)
                    .AddTrigger(
                        trigger =>
                            trigger.ForJob(jobKey)
                                .WithSimpleSchedule(
                                    schedule =>
                                        schedule.WithIntervalInSeconds(10)
                                            .RepeatForever()));

                configure.UseMicrosoftDependencyInjectionJobFactory();
            });

            services.AddQuartzHostedService();

            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<DomainEventsToOutboxMessagesInterceptor>();

            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                var interceptor = sp.GetService<DomainEventsToOutboxMessagesInterceptor>();

                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"))
                    .AddInterceptors(interceptor);
            });

            services.AddScoped<IExamRepository, ExamRepository>();
            services.AddScoped<IVoucherRepository, VoucherRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IEmailService, EmailService>();

            return services;
        }

        private static void AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptions = new JwtOptions();
            configuration.Bind(JwtOptions.SectionName, jwtOptions);

            services.AddSingleton(Options.Create(jwtOptions));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                });


            services.AddScoped<IJwtTokenProvider, JwtTokenProvider>();
            services.AddScoped<IRegisterService, RegisterService>();
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
