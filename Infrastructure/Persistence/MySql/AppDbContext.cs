using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.MySql
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Car> Cars => Set<Car>();
        public DbSet<Reservation> Reservations => Set<Reservation>();
        public DbSet<Location> Locations => Set<Location>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasMany(c => c.Reservations).WithOne(r => r.Car);
                entity.Property(e => e.Color).HasConversion(l => l.ToString(), l => Enum.Parse<Color>(l)).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Fuel).HasConversion(l => l.ToString(), l => Enum.Parse<FuelType>(l)).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Transmission).HasConversion(l => l.ToString(), l => Enum.Parse<TransmissionType>(l)).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Type).HasConversion(l => l.ToString(), l => Enum.Parse<CarTypeEnum>(l)).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Status).HasConversion(l => l.ToString(), l => Enum.Parse<CarStatus>(l)).IsRequired().HasMaxLength(100);

            });
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(15);
            });
            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Country).HasDefaultValue("Colombia").IsRequired().HasMaxLength(100);
                entity.Property(e => e.State).IsRequired().HasMaxLength(100);
                entity.Property(e => e.City).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Address).IsRequired().HasMaxLength(200);
            });
            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(r => r.Note).HasMaxLength(255);
                entity.HasOne(r => r.PickUpLocation).WithMany().HasForeignKey("PickUpLocationId").IsRequired();
                entity.HasOne(r => r.DropOffLocation).WithMany().HasForeignKey("DropOffLocationId").IsRequired();
                entity.OwnsOne(r => r.DateRange, dr =>
                {
                    dr.Property(p => p.StartDate)
                        .HasColumnName("StartDate")
                        .IsRequired();

                    dr.Property(p => p.EndDate)
                        .HasColumnName("EndDate")
                        .IsRequired();
                });
            });
        }
    }
}
