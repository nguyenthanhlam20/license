using DataAccess.LicenseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace DataAccess.LicenseContext
{
    public class LicenseFactory : IDesignTimeDbContextFactory<LicenseDbContext>
    {
        public LicenseDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DB");
            var optionBuilder = new DbContextOptionsBuilder<LicenseDbContext>();
            optionBuilder.UseSqlServer(connectionString);

            return new LicenseDbContext(optionBuilder.Options);
        }
    }
}
