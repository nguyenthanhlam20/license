using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;
using Microsoft.Extensions.Configuration;
using Data.Configurations;
using DataAccess.Configurations;
using DataAccess.Seeding;

namespace DataAccess.LicenseContext
{
    public class LicenseDbContext : IdentityDbContext<Account, IdentityRole<Guid>, Guid>
    {
        public LicenseDbContext(DbContextOptions options) : base(options)
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
            modelBuilder.ApplyConfiguration(new LicenseConfiguration());

            new AccountSeeder(modelBuilder).Seed();

            base.OnModelCreating(modelBuilder);
        }

        #region DbSet
        public DbSet<Account> Accounts { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<License> Licenses { get; set; }
   
        #endregion
    }
}