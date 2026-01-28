using Aplication.DTOs;
using Domain.Entities;
using Infrastructure.Persistence.MySql;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;

namespace IntegrationTests
{
    public class CarsControllerTests
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;

        public CarsControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Get_ShouldReturnAvailableCars()
        {
            Guid locationId;
            DateTime start;
            DateTime end;

            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                Location location = new("Cundinamarca", "Bogotá", "Cl 34", "test");
                context.Locations.Add(location);

                Car car = new(location, "SLD 345", "Toyota", "corolla", 2023, 250000);
                context.Cars.Add(car);
                await context.SaveChangesAsync();

                locationId = location.Id;
                start = DateTime.UtcNow.Date;
                end = start.AddDays(2);
            }

            var response = await _client.GetAsync($"/Cars?LocationId={locationId}&StartDate={start:o}&EndDate={end:o}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var cars = await response.Content
                .ReadFromJsonAsync<List<SearchCarsResponse>>();

            Assert.NotNull(cars);
            Assert.Single(cars);
            Assert.Equal("SLD 345", cars![0].Plate);            
        }
    }
}
