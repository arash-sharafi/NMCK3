using Microsoft.Extensions.DependencyInjection;
using NMCK3.Application;
using NMCK3.Application.Repositories;
using NMCK3.Infrastructure.Persistance;
using NMCK3.Infrastructure.Persistance.Repositories;

namespace NMCK3.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IExamRepository, IExamRepository>();
            services.AddScoped<IVoucherRepository, VoucherRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
