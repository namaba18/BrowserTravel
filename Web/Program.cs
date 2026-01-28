using Infrastructure;
using Infrastructure.Persistence.Mongo;
using Infrastructure.Persistence.Mongo.Seed;
using Infrastructure.Persistence.MySql.Seed;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("Mongo"));
builder.Services.AddSingleton<MongoContext>();

builder.Services.AddTransient<MongoSeed>();
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
