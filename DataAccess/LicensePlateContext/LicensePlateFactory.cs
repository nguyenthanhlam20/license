using DataAccess.LicensePlateContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace DataAccess.LicensePlateContext
{
    public class LicensePlateFactory : IDesignTimeDbContextFactory<LicensePlateDbContext>
    {
        public LicensePlateDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DB");
            var optionBuilder = new DbContextOptionsBuilder<LicensePlateDbContext>();
            optionBuilder.UseSqlServer(connectionString);

            return new LicensePlateDbContext(optionBuilder.Options);
        }
    }
}
