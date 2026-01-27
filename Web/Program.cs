using Aplication.Interfaces;
using Domain.Events;
using Infrastructure.Events;
using Infrastructure.Events.Handlers;
using Infrastructure.Persistence.Mongo;
using Infrastructure.Persistence.Mongo.Seed;
using Infrastructure.Persistence.MySql;
using Infrastructure.Persistence.MySql.Seed;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Pasar a infrastructure

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddTransient<MySqlSeed>();

builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("Mongo"));
builder.Services.AddSingleton<MongoContext>();

builder.Services.AddTransient<MongoSeed>();

builder.Services.AddScoped<IEventDispacher, InMemoryEventDispatcher>();
builder.Services.AddScoped<IDomainEventHandler<CreateResevationEvent>, CarReservedEventHandler>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var mysqlSeed = scope.ServiceProvider.GetRequiredService<MySqlSeed>();
    await mysqlSeed.SeedAsync();

    var seed = scope.ServiceProvider.GetRequiredService<MongoSeed>();
    await seed.SeedAsync();
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
