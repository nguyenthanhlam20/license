using DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Seeding
{
    public class SeriSeeder
    {
        private readonly ModelBuilder modelBuilder;

        public SeriSeeder(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }
        public void Seed()
        {
            // Seed role data
            modelBuilder.Entity<Seri>().HasData(
               new Seri() { SeriId = 1, Title = "A" },
               new Seri() { SeriId = 2, Title = "B" },
               new Seri() { SeriId = 3, Title = "C" },
               new Seri() { SeriId = 4, Title = "D" },
               new Seri() { SeriId = 5, Title = "E" },
               new Seri() { SeriId = 6, Title = "F" },
               new Seri() { SeriId = 7, Title = "G" }
            );
        }
    }
}
