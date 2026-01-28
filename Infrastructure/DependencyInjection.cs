using Aplication.Interfaces;
using Domain.Events;
using Infrastructure.Events;
using Infrastructure.Events.Handlers;
using Infrastructure.Persistence.Mongo;
using Infrastructure.Persistence.Mongo.Seed;
using Infrastructure.Persistence.MySql;
using Infrastructure.Persistence.MySql.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext(services, configuration);
            services.AddScoped<IEventDispatcher, InMemoryEventDispatcher>();
            services.AddScoped<IDomainEventHandler<CarResevedEvent>, CarReservedEventHandler>();
            return services;
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(12, 11, 0))));

            services.AddTransient<MySqlSeed>();

            
                     
        }

    }
}
