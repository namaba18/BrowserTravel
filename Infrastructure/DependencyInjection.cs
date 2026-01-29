using Aplication.Interfaces;
using Domain.Events;
using Domain.Interfaces.Repositories;
using Infrastructure.Events;
using Infrastructure.Events.Handlers;
using Infrastructure.Persistence.Mongo.Repositories;
using Infrastructure.Persistence.MySql;
using Infrastructure.Persistence.MySql.Repositories;
using Infrastructure.Persistence.MySql.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, bool isTest)
        {
            AddDbContext(services, configuration, isTest);
            services.AddScoped<IEventDispatcher, InMemoryEventDispatcher>();
            services.AddScoped<IDomainEventHandler<CarResevedEvent>, CarReservedEventHandler>();
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            return services;
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration, bool isTest)
        {
            if (!isTest)
            {
                var connectionString = configuration.GetConnectionString("Database");
                services.AddDbContext<AppDbContext>(options =>
                    options.UseMySql(connectionString, new MySqlServerVersion(new Version(12, 11, 0))));

                services.AddTransient<MySqlSeed>();
            }        
        }

    }
}
