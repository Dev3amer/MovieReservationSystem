using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MovieReservationSystem.Core.Behavior;
using System.Reflection;

namespace MovieReservationSystem.Core
{
    public static class ModuleCoreDependencies
    {
        public static void AddModuleCoreDependencies(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}
