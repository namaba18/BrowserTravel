using Aplication.Modules.Car;
using Aplication.Modules.Reservation;
using Infrastructure;
using Infrastructure.Persistence.Mongo;
using Infrastructure.Persistence.Mongo.Seed;
using Infrastructure.Persistence.MySql.Seed;

var builder = WebApplication.CreateBuilder(args);

bool isTest = builder.Environment.EnvironmentName == "IntegrationTests";
builder.Services.AddInfrastructure(builder.Configuration, isTest);
builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("Mongo"));
builder.Services.AddSingleton<MongoContext>();

builder.Services.AddTransient<MongoSeed>();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(SearchCarsQueryHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(CreateReservationCommand).Assembly);
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var mysqlSeed = scope.ServiceProvider.GetRequiredService<MySqlSeed>();
        await mysqlSeed.SeedAsync();

        var seed = scope.ServiceProvider.GetRequiredService<MongoSeed>();
        await seed.SeedAsync();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }