using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NMCK3.Application.Behaviors;

namespace NMCK3.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(DependencyInjection).Assembly);
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly,
                includeInternalTypes: true);
            return services;
        }
    }
}
