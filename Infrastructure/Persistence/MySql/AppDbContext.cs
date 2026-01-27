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
            });
            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.Id);
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
