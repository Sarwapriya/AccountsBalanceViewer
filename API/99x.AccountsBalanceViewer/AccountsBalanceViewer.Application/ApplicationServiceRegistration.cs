using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AccountsBalanceViewer.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //register mediatr
            services.AddMediatR(Assembly.GetExecutingAssembly());
            //register automapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}