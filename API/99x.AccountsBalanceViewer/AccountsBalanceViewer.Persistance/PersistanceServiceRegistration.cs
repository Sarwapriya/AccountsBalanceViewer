using AccountsBalanceViewer.Application.Contracts.Persistance;
using AccountsBalanceViewer.Persistance.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsBalanceViewer.Persistance
{
    public static class PersistanceServiceRegistration
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services)
        {
            //register mediatr
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IBalanceRepository, BalanceRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
