
using Domain.Entities;

namespace Infrastructure.Persistence.MySql.Seed
{
    public class MySqlSeed
    {
        private readonly AppDbContext _context;

        public MySqlSeed(AppDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await SeedLocations();
            await SeedCars();
            await SeedCustomer();
        }

        private async Task SeedCustomer()
        {
            if (_context.Customers.Any())
                return;

            List<Customer> customers =
            [
            new Customer("Juan", "Gomez", "juan@correo.com", "3001234567", "DL123456")
            ];
        }
            
        private async Task SeedCars()
        {
            if (_context.Locations.Any())
                return;

            var locations = new List<Location>
        {
            new Location( "Cundinamarca", "Bogotá", "Cl 4 34 56"){Name="Cundinamarca"},
            new Location( "Antioquia", "Medellin", "Cl 5 36 56"){Name="Medellin"},
            new Location( "Cauca", "Cali", "Cl 67 24 56"){Name="Cali"},
        };

            _context.Locations.AddRange(locations);
            await _context.SaveChangesAsync();
        }

        private async Task SeedLocations()
        {
            if (_context.Cars.Any())
                return;

            var bogota = _context.Locations.FirstOrDefault(l => l.City == "Bogotá");

            if (bogota == null)
                return;
            var cars = new List<Car>
            {
                new Car("SRC 455", "Mazda", "3", 2024, 250000){Location= bogota},
                new Car("ERV 455", "Chevrolet", "onix", 2025, 200000){Location= bogota},
                new Car("MTS 455", "Toyota", "hyluz", 2023, 350000){Location= bogota}
            };

            _context.Cars.AddRange(cars);
            await _context.SaveChangesAsync();
        }
    }
}
