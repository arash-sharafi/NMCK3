using Microsoft.Extensions.DependencyInjection;
using NMCK3.Api.Common.Mapping;

namespace NMCK3.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddControllers();

            services.AddMappings();

            return services;
        }
    }
}
