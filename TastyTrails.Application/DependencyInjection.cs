using FluentValidation;
using MediatR;
using System.Reflection;
using TastyTrails.Application.Common.Behaviours;
using TastyTrails.Application.Orders.Interfaces;
using TastyTrails.Application.Orders.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterApplication(this IServiceCollection services)
        {
            var applicationAssembly = Assembly.GetExecutingAssembly();

            services
                .AddMediatR(opts => 
                {
                    opts.RegisterServicesFromAssembly(applicationAssembly);
                    opts.AddBehavior(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));

                })
                .AddValidatorsFromAssembly(applicationAssembly)
                .AddAutoMapper(applicationAssembly);

            services.AddScoped<IOrderService, OrderService>();

            return services;
        }
    }
}
