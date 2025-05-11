using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitiesManager.Core.Entity_Classes;
using Microsoft.EntityFrameworkCore;

namespace CityManager.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {


        public virtual DbSet<City> Cities { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<City>().HasKey(c => c.CityId);
            modelBuilder.Entity<City>().Property(c => c.CityName).IsRequired();

            modelBuilder.Entity<City>().HasData(
                new City
                {
                    CityId = Guid.Parse("37DC186D-763C-4854-BD8E-F087C91FCF0E"),
                    CityName = "New York"
                });
            modelBuilder.Entity<City>().HasData(
                new City
                {
                    CityId = Guid.Parse("A1B2C3D4-E5F6-7890-ABCD-EF1234567890"),
                    CityName = "Los Angeles"
                });
            modelBuilder.Entity<City>().HasData(
                new City
                {
                    CityId = Guid.Parse("B2C3D4E5-F6A7-8901-BCDE-F1234567890A"),
                    CityName = "Chicago"
                }
            );

        }


    }
}
