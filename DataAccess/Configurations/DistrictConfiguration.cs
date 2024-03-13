using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    public class DistrictConfiguration : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.ToTable(nameof(District));
            builder.HasKey(x => x.DistrictId);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(5000);
            builder.Property(x => x.Prefix).IsRequired().HasMaxLength(5000);
         
        }
    }
}
