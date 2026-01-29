using Aplication.Modules.Reservation;
using Domain.Entities;
using Infrastructure.Persistence.MySql;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;

namespace IntegrationTests
{
    public class ReservationsControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;

        public ReservationsControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
            using var scope = factory.Services.CreateScope();
            _factory = factory; ;
        }

        [Fact]
        public async Task Post_ShouldReturnOk_WhenReservationIsCreated()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                Location location = new("Colombia", "Cundinamarca", "Bogotá", "Cl 34", "test");
                context.Locations.Add(location);

                Car car = new(location, "SLD 345", "Toyota", "corolla", 2023, 250000) ;
                context.Cars.Add(car);

                Customer customer = new("Juan", "Perez", "3453453", "juan@correo.com", "ASD34534");
                context.Customers.Add(customer);

                context.SaveChanges();

                var command = new CreateReservationCommand
                {
                    CarId = car.Id,
                    CustomerId = customer.Id,
                    PickUpLocationId = location.Id,
                    DropOffLocationId = location.Id,
                    Start = DateTime.Today,
                    End = DateTime.Today.AddDays(2)
                };


                var response = await _client.PostAsJsonAsync("/Reservations", command);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var result = await response.Content.ReadFromJsonAsync<bool>();
                Assert.True(result);
            }
        }
    }
}
