using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;
using Microsoft.Extensions.Configuration;
using Data.Configurations;
using DataAccess.Configurations;
using DataAccess.Seeding;

namespace DataAccess.LicensePlateContext
{
    public class LicensePlateDbContext : IdentityDbContext<Account, IdentityRole<Guid>, Guid>
    {
        public LicensePlateDbContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Get connection strings in appsetings.json
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                                 .SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                IConfigurationRoot configuration = builder.Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DB"));
            }
        }
        /// <summary>
        /// Apply db configuration
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new DistrictConfiguration());
            modelBuilder.ApplyConfiguration(new LicensePlateConfiguration());
            modelBuilder.ApplyConfiguration(new SeriesConfiguration());

            new AccountSeeder(modelBuilder).Seed();
            new DistrictSeeder(modelBuilder).Seed();
            new SeriSeeder(modelBuilder).Seed();

            base.OnModelCreating(modelBuilder);
        }

        #region DbSet
        public DbSet<Account> Accounts { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<LicensePlate> LicensePlates { get; set; }
        public DbSet<Seri> Series { get; set; }
   
        #endregion
    }
}