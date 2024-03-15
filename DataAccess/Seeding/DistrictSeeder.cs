using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Seeding
{
    public class DistrictSeeder
    {
        private readonly ModelBuilder modelBuilder;

        public DistrictSeeder(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }
        public void Seed()
        {
            // Seed role data
            modelBuilder.Entity<District>().HasData(
               new District() { DistrictId = 1, Name = "Hà Nội", Prefix = "1" },
               new District() { DistrictId = 2, Name = "Hải Phòng", Prefix = "2" },
               new District() { DistrictId = 3, Name = "Hải Dương", Prefix = "3" }

            );
        }
    }
}
