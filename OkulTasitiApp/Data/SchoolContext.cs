using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OkulTasitiApp.Models;

namespace OkulTasitiApp.Data
{
    public class SchoolContext : IdentityDbContext<ApplicationUser>
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }
        public DbSet<SchoolDriver> SchoolDrivers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<School> School { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SchoolDriver>()
                .HasKey(sd => new { sd.SchoolID, sd.DriverID });

            // Diğer model konfigürasyonları...
        }
    }
}
