using Microsoft.EntityFrameworkCore;
using Vuew.Models;

namespace Vuew.Infrastructure
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetHotelConfiguration(modelBuilder);

            var room = modelBuilder.Entity<Room>();

            room
                .ToTable("Rooms");

            room
                .HasKey(r => r.Id);

            room
                .Property(r => r.Name)
                .HasMaxLength(256)
                .IsRequired();
        }

        private static void SetHotelConfiguration(ModelBuilder modelBuilder)
        {
            var hotel = modelBuilder.Entity<Hotel>();

            hotel
                .ToTable("Hotels");

            hotel
                .HasKey(h => h.Id);

            hotel
                .Property(h => h.Name)
                .HasMaxLength(256)
                .IsRequired();

            hotel
                .Property(h => h.City)
                .HasMaxLength(256)
                .IsRequired();
        }
    }
}
